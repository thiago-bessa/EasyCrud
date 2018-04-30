using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Data;
using EasyCrud.Model.Attributes;
using EasyCrud.Model.Data;
using EasyCrud.Model.Exceptions;
using EasyCrud.Model.Helpers;
using EasyCrud.Model.Info;
using EasyCrud.Model.Interfaces;
using EasyCrud.Model.ViewData;
using EasyCrud.Model.Workflow;

namespace EasyCrud.Workflow
{
    public class PageWorkflow : IPageWorkflow
    {
        #region Private Fields

        private readonly DbContextInfo _dbContextInfo;
        private readonly string _routeAlias;

        #endregion

        #region Constructors

        public PageWorkflow(WorkflowParameters parameters)
        {
            _dbContextInfo = ReflectionTools.GetDbContextInfo(parameters.AssemblyName, parameters.ContextName);
            _routeAlias = parameters.RouteAlias;
        }

        #endregion
        
        #region Public Methods

        public PageViewData GetPageViewData()
        {
            return CreatePageViewData();
        }

        public PageViewData GetPageViewData(string repositoryName)
        {
            return CreatePageViewData(repositoryName);
        }

        public PageViewData GetPageViewData(string repositoryName, string id)
        {
            return CreatePageViewData(repositoryName, id);
        }

        #endregion

        #region ViewData Methods

        private PageViewData CreatePageViewData(string repositoryName = null, string id = null)
        {
            var pageViewData = new PageViewData
            {
                Menu = new MenuViewData(_dbContextInfo, _routeAlias, repositoryName)
            };

            if (!string.IsNullOrWhiteSpace(repositoryName))
            {
                var repositoryInfo = _dbContextInfo.GetRepository(repositoryName);
                var dataRepository = new EasyCrudRepository(_dbContextInfo.DbContext, repositoryInfo);

                pageViewData.Name = repositoryName;
                pageViewData.MainComponent = CreateMainComponentViewData(repositoryInfo, dataRepository, id);

                if (!string.IsNullOrWhiteSpace(id))
                {
                    pageViewData.Components = CreateChildrenComponentViewData(repositoryInfo, dataRepository, id);
                }
            }


            return pageViewData;
        }
        
        private ComponentViewData CreateMainComponentViewData(RepositoryInfo repositoryInfo, IEasyCrudRepository dataRepository, string id)
        {
            var componentViewData = new ComponentViewData();

            var fieldsViewData = repositoryInfo.Columns.Select(CreateFieldViewData).ToList();

            var criteria = new Criteria
            {
                Columns = fieldsViewData.Select(f => f.Column).ToList()
            };
            
            componentViewData.Title = repositoryInfo.Label;
            componentViewData.Fields = fieldsViewData;

            if (id == null)
            {
                var entities = dataRepository.List(criteria);
                componentViewData.Entities = entities.Select(e => new EntityViewData { Id = e.Id, Data = e.Data }).ToList();
            }

            return componentViewData;
        }

        private List<ComponentViewData> CreateChildrenComponentViewData(RepositoryInfo repositoryInfo, IEasyCrudRepository dataRepository, string id)
        {
            var viewDatas = new List<ComponentViewData>();

            var components = repositoryInfo.Type.GetProperties().Where(p => Attribute.IsDefined(p, typeof(BaseComponentAttribute), true));

            foreach (var component in components)
            {
                if (component.PropertyType.GetGenericTypeDefinition() != typeof(ICollection<>))
                {
                    throw new PropertyConfigurationException($"Property '{component.Name}', with ComponentAttribute, is not an ICollection<>");
                }

                var componentAttribute = component.GetCustomAttribute<BaseAttribute>(true);
                var componentType = component.PropertyType.GenericTypeArguments.First();
                viewDatas.Add(CreateChildComponentViewData(componentType, componentAttribute.Label, componentAttribute.Order));
            }

            return viewDatas;
        }

        private ComponentViewData CreateChildComponentViewData(Type repositoryType, string label, int order)
        {
            var fields = repositoryType.GetProperties().Where(p => Attribute.IsDefined(p, typeof(BaseFieldAttribute), true));

            return new ComponentViewData
            {
                Title = label,
                Order = order,
                //Fields = fields.Select(CreateFieldViewData).ToList()
            };
        }

        private FieldViewData CreateFieldViewData(ColumnInfo columnInfo)
        {
            var attribute = columnInfo.PropertyInfo.GetCustomAttribute<BaseFieldAttribute>(true);

            var fieldViewData = new FieldViewData
            {
                Label = attribute.Label,
                ViewFile = attribute.ViewFile,
                Order = attribute.Order,
                ReadOnly = attribute.ReadOnly,
                Column = columnInfo.Name
            };
            
            switch (attribute)
            {
                case BooleanAttribute boolean:
                    break;

                case DateTimeAttribute date:
                    break;

                case ImageAttribute image:
                    break;

                case SelectionAttribute selection:
                    break;

                case TextAttribute text:
                    break;

            }

            return fieldViewData;
        }

        #endregion
    }
}

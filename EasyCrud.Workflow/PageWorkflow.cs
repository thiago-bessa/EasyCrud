using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.DAO;
using EasyCrud.Model.Attributes;
using EasyCrud.Model.Exceptions;
using EasyCrud.Model.Helpers;
using EasyCrud.Model.Info;
using EasyCrud.Model.Interfaces;
using EasyCrud.Model.ViewData;

namespace EasyCrud.Workflow
{
    public class PageWorkflow : IPageWorkflow
    {
        #region Private Fields

        private readonly DbContextInfo _dbContextInfo;

        #endregion

        #region Constructors

        public PageWorkflow(string assemblyName, string contextName)
        {
            _dbContextInfo = ReflectionTools.GetDbContextInfo(assemblyName, contextName);
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

        public PageViewData GetPageViewData(string repositoryName, int id)
        {
            return CreatePageViewData(repositoryName, id);
        }

        #endregion

        #region ViewData Methods

        private PageViewData CreatePageViewData(string repositoryName = null, int? id = null)
        {
            var pageViewData = new PageViewData
            {
                Menu = new MenuViewData(_dbContextInfo)
            };

            if (!string.IsNullOrWhiteSpace(repositoryName))
            {
                var repositoryInfo = _dbContextInfo.GetRepository(repositoryName);
                var dataRepository = new EasyCrudRepository(_dbContextInfo.DbContext, repositoryInfo);

                pageViewData.MainComponent = CreateMainComponentViewData(repositoryInfo, dataRepository, id);
                pageViewData.Components = CreateChildrenComponentViewData(repositoryInfo, dataRepository, id);
            }

            return pageViewData;
        }
        
        private ComponentViewData CreateMainComponentViewData(RepositoryInfo repositoryInfo, IEasyCrudRepository dataRepository, int? id)
        {
            var fields = repositoryInfo.Type.GetProperties().Where(p => Attribute.IsDefined(p, typeof(BaseFieldAttribute), true));

            return new ComponentViewData
            {
                Title = repositoryInfo.Label,
                Fields = fields.Select(CreateFieldViewData).ToList(),
                Entities = dataRepository.List()
            };
        }

        private List<ComponentViewData> CreateChildrenComponentViewData(RepositoryInfo repositoryInfo, IEasyCrudRepository dataRepository, int? id)
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
                Fields = fields.Select(CreateFieldViewData).ToList()
            };
        }

        private FieldViewData CreateFieldViewData(PropertyInfo field)
        {
            var attribute = field.GetCustomAttribute<BaseFieldAttribute>(true);

            var fieldViewData = new FieldViewData
            {
                Label = attribute.Label,
                ViewFile = attribute.ViewFile,
                Order = attribute.Order,
                ReadOnly = attribute.ReadOnly,
                Column = field.Name
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

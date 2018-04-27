using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Attributes;
using EasyCrud.Model.Exceptions;
using EasyCrud.Model.Interfaces;
using EasyCrud.Model.ViewData;
using EasyCrud.Workflow.Model;

namespace EasyCrud.Workflow
{
    public class PageWorkflow : IPageWorkflow
    {
        #region Public Methods

        public PageViewData GetPageViewData(string assemblyName, string contextName)
        {
            return CreatePageViewData(assemblyName, contextName);
        }

        public PageViewData GetPageViewData(string assemblyName, string contextName, string dbSet)
        {
            return CreatePageViewData(assemblyName, contextName, dbSet);
        }

        public PageViewData GetPageViewData(string assemblyName, string contextName, string dbSet, int id)
        {
            return CreatePageViewData(assemblyName, contextName, dbSet, id);
        }

        #endregion

        #region ViewData Methods

        private static PageViewData CreatePageViewData(string assemblyName, string contextName, string repositoryName = null, int? id = null)
        {
            var pageViewData = new PageViewData();

            var context = GetDbContext(assemblyName, contextName);
            var repositories = GetRepositories(context);

            pageViewData.Menu = CreateMenuViewData(repositories);

            if (string.IsNullOrWhiteSpace(repositoryName))
            {
                
            }
            else
            {
                var repository = GetRepository(repositories, contextName, repositoryName);

                pageViewData.MainComponent = CreateComponentViewData(repository.Type, id);
                pageViewData.Components = CreateChildrenComponentViewData(repository.Type, id);
            }

            return pageViewData;
        }

        private static MenuViewData CreateMenuViewData(Dictionary<string, RepositoryInfo> repositories)
        {
            return new MenuViewData
            {
                MenuItems = repositories.Select(repository => CreateMenuItemViewData(repository.Value))
            };
        }

        private static MenuItemViewData CreateMenuItemViewData(RepositoryInfo repository)
        {
            return new MenuItemViewData
            {
                Label = repository.Label,
                Repository = repository.Name,
                Order = repository.Order
            };
        }

        private static ComponentViewData CreateComponentViewData(Type repositoryType, int? id)
        {
            var fields = repositoryType.GetProperties().Where(p => Attribute.IsDefined(p, typeof(BaseFieldAttribute), true));

            return new ComponentViewData
            {
                 Fields = fields.Select(CreateFieldViewData).ToList()
            };
        }

        private static List<ComponentViewData> CreateChildrenComponentViewData(Type repositoryType, int? id)
        {
            var components = repositoryType.GetProperties().Where(p => Attribute.IsDefined(p, typeof(BaseComponentAttribute), true)).ToArray();

            if (components.Any(c => c.PropertyType.GetGenericTypeDefinition() != typeof(ICollection<>)))
            {
                throw new PropertyConfigurationException("Property with ComponentAttribute is not an ICollection<>");
            }

            return components
                .Select(property => property.PropertyType.GenericTypeArguments.First())
                .Select(component => CreateComponentViewData(component, id))
                .ToList();
        }
        
        private static FieldViewData CreateFieldViewData(PropertyInfo field)
        {
            var attributes = field.GetCustomAttributes(typeof(BaseFieldAttribute), true);
            var fieldViewData = new FieldViewData();

            foreach (var attribute in attributes)
            {
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
            }

            return fieldViewData;
        }

        #endregion

        #region Reflection Methods

        private static RepositoryInfo GetRepository(IReadOnlyDictionary<string, RepositoryInfo> repositories, string contextName, string repositoryName)
        {
            if (!repositories.ContainsKey(repositoryName))
            {
                throw new RepositoryNotFoundException(contextName, repositoryName);
            }

            return repositories[repositoryName];
        }

        private static Dictionary<string, RepositoryInfo> GetRepositories(Type context)
        {
            return context
                .GetProperties()
                .Where(p => p.PropertyType.IsGenericType)
                .Where(p => p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) && p.CustomAttributes.Any(c => c.AttributeType == typeof(RepositoryAttribute)))
                .ToDictionary(p => p.Name, GetRepositoryInfo);
        }

        private static RepositoryInfo GetRepositoryInfo(PropertyInfo repository)
        {
            var repositoryAttribute = repository.GetCustomAttribute<RepositoryAttribute>();
            
            return new RepositoryInfo
            {
                Name = repository.Name,
                Label = repositoryAttribute.Label,
                Order = repositoryAttribute.Order,
                Type = repository.PropertyType.GenericTypeArguments.First()
            };
        }

        private static Type GetDbContext(string assemblyName, string contextName)
        {
            var assembly = Assembly.Load(assemblyName);
            return assembly.GetType(contextName);
        }

        #endregion
    }
}

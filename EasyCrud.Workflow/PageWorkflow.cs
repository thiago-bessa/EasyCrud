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

        private static PageViewData CreatePageViewData(string assemblyName, string contextName, string setName = null, int? id = null)
        {
            var pageViewData = new PageViewData();

            var context = GetDbContext(assemblyName, contextName);
            var dbSets = GetDbSets(context);

            pageViewData.Menu = CreateMenuViewData(dbSets);

            if (string.IsNullOrWhiteSpace(setName))
            {
                
            }
            else
            {
                var dbSetType = GetDbSetType(dbSets, contextName, setName);

                pageViewData.MainComponent = CreateComponentViewData(dbSetType, id);
                pageViewData.Components = CreateChildrenComponentViewData(dbSetType, id);
            }

            return pageViewData;
        }

        private static MenuViewData CreateMenuViewData(Dictionary<string, Type> dbSets)
        {
            return new MenuViewData
            {
                MenuItems = dbSets.Select(set => set.Key)
            };
        }

        private static ComponentViewData CreateComponentViewData(Type type, int? id)
        {
            var properties = type.GetProperties().Where(p => Attribute.IsDefined(p, typeof(BaseFieldAttribute), true));

            return new ComponentViewData
            {
                 Fields = properties.Select(CreateFieldViewData).ToList()
            };
        }

        private static List<ComponentViewData> CreateChildrenComponentViewData(Type type, int? id)
        {
            var components = type.GetProperties().Where(p => Attribute.IsDefined(p, typeof(BaseComponentAttribute), true)).ToArray();

            if (components.Any(c => c.PropertyType.GetGenericTypeDefinition() != typeof(ICollection<>)))
            {
                throw new PropertyConfigurationException("Property with ComponentAttribute is not an ICollection<>");
            }

            return components
                .Select(property => property.PropertyType.GenericTypeArguments.First())
                .Select(component => CreateComponentViewData(component, id))
                .ToList();
        }
        
        private static FieldViewData CreateFieldViewData(PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes(typeof(BaseFieldAttribute), true);
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

        private static Type GetDbSetType(IReadOnlyDictionary<string, Type> dbSets, string contextName, string setName)
        {
            if (!dbSets.ContainsKey(setName))
            {
                throw new DbSetNotFoundException(contextName, setName);
            }

            return dbSets[setName];
        }

        private static Dictionary<string, Type> GetDbSets(Type context)
        {
            return context
                .GetProperties()
                .Where(p => p.PropertyType.IsGenericType)
                .Where(p => p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .ToDictionary(p => p.Name, p => p.PropertyType.GenericTypeArguments.First());
        }

        private static Type GetDbContext(string assemblyName, string contextName)
        {
            var assembly = Assembly.Load(assemblyName);
            return assembly.GetType(contextName);
        }

        #endregion
    }
}

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

        private PageViewData CreatePageViewData(string assemblyName, string contextName, string setName = null, int? id = null)
        {
            var pageViewData = new PageViewData();

            var context = GetDbContext(assemblyName, contextName);
            var dbSets = GetDbSets(context);

            pageViewData.Menu = GetMenuViewData(dbSets);

            if (!string.IsNullOrWhiteSpace(setName))
            {
                var dbSetType = GetDbSetType(dbSets, contextName, setName);

                pageViewData.MainComponent = GetComponentViewData(dbSetType, id);
                pageViewData.Components = GetChildrenComponentViewData(dbSetType, id);
            }

            return pageViewData;
        }

        private ComponentViewData GetComponentViewData(Type type, int? id)
        {
            var componentViewData = new ComponentViewData();

            var properties = type.GetProperties().Where(p => Attribute.IsDefined(p, typeof(BaseFieldAttribute), true));
            componentViewData.Fields.AddRange(properties.Select(GetFieldViewData));

            return componentViewData;
        }

        private List<ComponentViewData> GetChildrenComponentViewData(Type type, int? id)
        {
            var components = type.GetProperties().Where(p => Attribute.IsDefined(p, typeof(BaseComponentAttribute), true)).ToArray();

            if (components.Any(c => c.PropertyType.GetGenericTypeDefinition() != typeof(ICollection<>)))
            {
                throw new PropertyConfigurationException("Property with ComponentAttribute is not an ICollection<>");
            }

            return components
                .Select(property => property.PropertyType.GenericTypeArguments.First())
                .Select(component => GetComponentViewData(component, id))
                .ToList();
        }
        
        private FieldViewData GetFieldViewData(PropertyInfo property)
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

                    case SelectAttribute select:
                        break;

                    case TextAttribute text:
                        break;
                }
            }

            return fieldViewData;
        }

        private static MenuViewData GetMenuViewData(Dictionary<string, Type> dbSets)
        {
            return new MenuViewData
            {
                MenuItems = dbSets.Select(set => set.Key)
            };
        }

        private static Type GetDbSetType(Dictionary<string, Type> dbSets, string contextName, string setName)
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
    }
}

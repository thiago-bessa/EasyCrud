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
            var dbSets = GetDbSets(assemblyName, contextName);
            return new PageViewData();
        }

        public PageViewData GetPageViewData(string assemblyName, string contextName, string dbSet)
        {
            var dbSetType = GetDbSetType(assemblyName, contextName, dbSet);
            return GetPageViewData(dbSetType, null);
        }

        public PageViewData GetPageViewData(string assemblyName, string contextName, string dbSet, int id)
        {
            var dbSetType = GetDbSetType(assemblyName, contextName, dbSet);
            return GetPageViewData(dbSetType, id);
        }

        private PageViewData GetPageViewData(Type type, int? id)
        {
            var pageViewData = new PageViewData
            {
                MainComponent = GetComponentViewData(type),
                Components = GetChildrenComponentViewData(type)
            };

            return pageViewData;
        }

        private ComponentViewData GetComponentViewData(Type type)
        {
            var componentViewData = new ComponentViewData();

            var properties = type.GetProperties().Where(p => Attribute.IsDefined(p, typeof(BaseFieldAttribute), true));
            componentViewData.Fields.AddRange(properties.Select(GetFieldViewData));

            return componentViewData;
        }

        private List<ComponentViewData> GetChildrenComponentViewData(Type type)
        {
            var components = type.GetProperties().Where(p => Attribute.IsDefined(p, typeof(BaseComponentAttribute), true)).ToArray();

            if (components.Any(c => c.PropertyType.GetGenericTypeDefinition() != typeof(ICollection<>)))
            {
                throw new PropertyConfigurationException("Property with ComponentAttribute is not an ICollection<>");
            }

            return components
                .Select(component => component.PropertyType.GenericTypeArguments.First())
                .Select(GetComponentViewData)
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

        private Type GetDbSetType(string assemblyName, string contextName, string dbSet)
        {
            var context = GetDbContext(assemblyName, contextName);
            var property = context.GetProperty(dbSet);

            if (property == null)
            {
                throw new DbSetNotFoundException(contextName, dbSet);
            }

            if (property.PropertyType.GetGenericTypeDefinition() != typeof(DbSet<>))
            {
                throw new PropertyConfigurationException($"'{dbSet}' is not a DbSet<>");
            }

            return property.PropertyType.GenericTypeArguments.First();
        }

        private IEnumerable<Type> GetDbSets(string assemblyName, string contextName)
        {
            var context = GetDbContext(assemblyName, contextName);

            return context
                .GetProperties()
                .Where(p => p.PropertyType.IsGenericType)
                .Where(p => p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .Select(p => p.PropertyType.GenericTypeArguments.First());
        }

        private Type GetDbContext(string assemblyName, string contextName)
        {
            var assembly = Assembly.Load(assemblyName);
            return assembly.GetType(contextName);
        }
    }
}

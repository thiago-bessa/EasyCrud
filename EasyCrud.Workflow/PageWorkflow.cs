using System;
using System.Collections.Generic;
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
        public PageViewData GetPageViewData(Type type)
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
    }
}

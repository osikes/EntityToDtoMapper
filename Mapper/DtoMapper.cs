using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EntityToDtoMapper
{
    public class DtoMapper
    {

        private static bool IsPropertyTagged(PropertyInfo property)
        {
            foreach (object attribute in property.GetCustomAttributes(true))
            {
                if (attribute is MapMe)
                {
                    return true;
                }
            }
            return false;
        }

        public static Dto Map<Entity, Dto>(object instance)
        {

            var target = (Dto)Activator.CreateInstance(typeof(Dto));

            foreach (var entityProperty in (typeof(Entity).GetProperties()))
            {
                if (IsPropertyTagged(entityProperty))
                {
                    var entityValue = entityProperty.GetValue(instance, new object[] { });

                    foreach (var targetProperty in (typeof(Dto).GetProperties()))
                    {
                        if (targetProperty.Name == entityProperty.Name)
                        {
                            var targetProp = target.GetType().GetProperties().Where(p => p.Name == targetProperty.Name).Single();
                            targetProp.SetValue(target, entityValue, new object[] { });
                        }
                    }


                }
            }

            return target;
        }
    }
}

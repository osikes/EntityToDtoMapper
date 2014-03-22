using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EntityToDtoMapper
{
    internal class MapReturn
    {
        
        public MapMe MapMe{get;set;}
        public bool Found { get; set; }
    }

    public class DtoMapper
    {
        private static MapReturn IsPropertyTagged(PropertyInfo property)
        {
            foreach (object attribute in property.GetCustomAttributes(true))
            {
                if (attribute is MapMe)
                {
                    return new MapReturn()
                    {
                        MapMe = (MapMe)attribute,
                        Found = true
                    };
                }
            }
            return new MapReturn()
            {
                MapMe = null,
                Found = false
            };
        }

        static bool GetValue(object currentObject, string propName, out object value,string entityName="")
        {
            // call helper function that keeps track of which objects we've seen before
            return GetValue(currentObject, propName, out value, new HashSet<object>(),entityName);
        }

        static bool GetValue(object currentObject, string propName, out object value,
                             HashSet<object> searchedObjects,string entityName="")
        {
            PropertyInfo propInfo = currentObject.GetType().GetProperty(propName);
            if (propInfo != null && (String.IsNullOrEmpty(entityName) || propInfo.Name == entityName))
            {
                value = propInfo.GetValue(currentObject, null);
                return true;
            }
            // search child properties
            foreach (PropertyInfo propInfo2 in currentObject.GetType().GetProperties())
            {   // ignore indexed properties
                if (propInfo2.GetIndexParameters().Length == 0)
                {
                    object newObject = propInfo2.GetValue(currentObject, null);
                    if (newObject != null && searchedObjects.Add(newObject) &&
                        GetValue(newObject, propName, out value, searchedObjects))
                        return true;
                }
            }
            // property not found here
            value = null;
            return false;
        }

        public static Dto Map<Entity,Dto>(object instance)
        {
            var target = (Dto)Activator.CreateInstance(typeof(Dto));

            foreach (var dtoProperty in (typeof(Dto).GetProperties()))
            {
                var MapReturnResult = IsPropertyTagged(dtoProperty);
                if (MapReturnResult.Found)
                {
                    object newValue;

                    GetValue(instance, (MapReturnResult.MapMe.FindEntity.Length > 0? MapReturnResult.MapMe.FindProperty : dtoProperty.Name), out newValue,MapReturnResult.MapMe.FindEntity);
                    var targetProp = target.GetType().GetProperties().Where(p => p.Name == dtoProperty.Name).Single();

                    if(newValue.GetType().GetProperties().Count() == 0 ||
                        (newValue.GetType().GetProperties().Any(prop=>!IsPropertyTagged(prop).Found)))//child property contains properties other than MapMe attribute
                    {
                       targetProp.SetValue(target, newValue, new object[] { });
                    }
                    else{
                        if (newValue.GetType().GetProperties().Count() > 0)//we're assuming the proprety is flagged as MapMe is itself a class instance and needs to to explored
                        {
                            var dtoTypeArgument = Type.GetType(dtoProperty.PropertyType.FullName);
                            
                            var entityTypeArgument= Type.GetType(newValue.GetType().FullName);

                            var mapMethod = typeof(DtoMapper).GetMethod(System.Reflection.MethodBase.GetCurrentMethod().Name);

                            var genericMapMethod = mapMethod.MakeGenericMethod(entityTypeArgument, dtoTypeArgument);

                            var result = genericMapMethod.Invoke(newValue, new object[] { newValue });

                            targetProp.SetValue(target, result, new object[] { });
                        }
                    }
                }
            }
            return target;
        }
    }
}

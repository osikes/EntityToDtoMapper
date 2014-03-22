using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EntityToDtoMapper
{
    public class MapReturn
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
                    //look for addition info
                    //var internalProperties = attribute.GetType().GetProperties().Where(p => p.Name == "FindEntity"|| p.Name== "FindProperty");

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

        static bool GetValue(object currentObject, string propName, out object value)
        {
            // call helper function that keeps track of which objects we've seen before
            return GetValue(currentObject, propName, out value, new HashSet<object>());
        }

        static bool GetValue(object currentObject, string propName, out object value,
                             HashSet<object> searchedObjects)
        {
            PropertyInfo propInfo = currentObject.GetType().GetProperty(propName);
            if (propInfo != null)
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

        public static Dto Map<Entity, Dto>(object instance)
        {
            var target = (Dto)Activator.CreateInstance(typeof(Dto));

            foreach (var dtoProperty in (typeof(Dto).GetProperties()))
            {
                var MapReturnResult = IsPropertyTagged(dtoProperty);
                if (MapReturnResult.Found)
                {
                    if (MapReturnResult.MapMe.FindEntity.Length > 0)
                    {

                        foreach (var entityProperty in (typeof(Entity).GetProperties()))
                        {
                            if (MapReturnResult.MapMe.FindEntity == entityProperty.Name)   //look for property matching Entity
                            { }
                            object newObject = entityProperty.GetValue(instance, null);

                            if (newObject != null)
                            {
                                PropertyInfo propInfo = newObject.GetType().GetProperty(MapReturnResult.MapMe.FindProperty);
                                if (propInfo != null)
                                {
                                    var entityValue = propInfo.GetValue(newObject, null);
                                    var targetProp = target.GetType().GetProperties().Where(p => p.Name == dtoProperty.Name).Single();
                                    targetProp.SetValue(target, entityValue, new object[] { });
                                }
                            }
                        }
                    }

                    else
                    {
                        //standard case
                        foreach (var entityProperty in (typeof(Entity).GetProperties()))
                        {
                            if (dtoProperty.Name == entityProperty.Name)
                            {
                                var entityValue = entityProperty.GetValue(instance, new object[] { });
                                var targetProp = target.GetType().GetProperties().Where(p => p.Name == dtoProperty.Name).Single();
                                targetProp.SetValue(target, entityValue, new object[] { });
                            }
                        }
                    }
                }
            }
            return target;
        }
    }
}

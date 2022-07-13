using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyMicroservice.Models
{
    public static class ControllerMethods
    {
        public const string CREATE_PROPERTY = "CreateProperty";
        public const string GET_ALL_PROPERTIES = "GetAllProperties";
        public const string GET_ALL_PROPERTIES_BY_TYPE = "getAllPropertiesByType";
        public const string GET_ALL_PROPERTIES_BY_LOCALITY = "getAllPropertiesByLocality";
        public const string DISPLAY_PROPERTY_BY_ID = "DisplayPropertyById";

        private static readonly List<string> _Methods = new List<string>()
        {
             CREATE_PROPERTY,GET_ALL_PROPERTIES,GET_ALL_PROPERTIES_BY_TYPE, GET_ALL_PROPERTIES_BY_LOCALITY , DISPLAY_PROPERTY_BY_ID
        };

        public static bool Exists(string methodName)
        {
            return _Methods.Contains(methodName);
        }
    }
}

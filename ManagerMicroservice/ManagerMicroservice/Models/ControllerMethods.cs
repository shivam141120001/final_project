using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerMicroservice.Models
{
    public static class ControllerMethods
    {
        public const string LOGIN = "Login";
        public const string VALIDATE = "Validate";
        public const string CREATE_EXECUTIVE = "CreateExecutive";
        public const string GET_ALL_EXECUTIVES = "GetAllExecutives";
        public const string GET_ALL_EXECUTIVES_BY_LOCALITY = "GetAllExecutivesByLocality";
        public const string GET_CUSTOMER_BY_ID = "GetCustomersById";
        public const string GET_ALL_CUSTOMERS = "GetAllCustomers";
        public const string GET_EXECUTIVE_BY_ID = "GetExecutiveById";
        public const string CREATE_PROPERTY = "CreateProperty";
        public const string ASSIGN_EXECUTIVE= "AssignExecutive";
        public const string GET_ALL_CUSTOMERS_BY_EXECUTIVE = "GetAllCustomersByExecutive";
        public const string DISPLAY_PROPERTY_BY_ID = "DisplayPropertyById";
        public const string DISPLAY_PROPERTY_BY_LOCALITY = "DisplayPropertyByLocality";
        public const string DISPLAY_PROPERTY_BY_TYPE = "DisplayPropertyByType";


        private static readonly List<string> _Methods = new List<string>()
        {
            LOGIN, VALIDATE, CREATE_EXECUTIVE, GET_ALL_EXECUTIVES, GET_ALL_EXECUTIVES_BY_LOCALITY, GET_CUSTOMER_BY_ID, GET_ALL_CUSTOMERS, GET_EXECUTIVE_BY_ID,
            ASSIGN_EXECUTIVE,GET_ALL_CUSTOMERS_BY_EXECUTIVE,DISPLAY_PROPERTY_BY_LOCALITY,DISPLAY_PROPERTY_BY_TYPE, DISPLAY_PROPERTY_BY_ID
        };

        public static bool Exists(string methodName)
        {
            return _Methods.Contains(methodName);
        }
    }
}

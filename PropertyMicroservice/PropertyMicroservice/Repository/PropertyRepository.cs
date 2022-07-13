using Newtonsoft.Json;
using PropertyMicroservice.Context;
using PropertyMicroservice.Models;
using PropertyMicroservice.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace PropertyMicroservice.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PropertyMicroserviceDbContext _propertyMicroserviceDbContext;
        public PropertyRepository(PropertyMicroserviceDbContext propertyMicroserviceDbContext)
        {
            _propertyMicroserviceDbContext = propertyMicroserviceDbContext;
        }

        
        

        public string AddProperty(Property property)
        {
            try
            {
                if(_propertyMicroserviceDbContext.Properties.Any(p=>p.PropertyId == property.PropertyId)) 
                {
                    return "false";
                }
                _propertyMicroserviceDbContext.Properties.Add(property);
                _propertyMicroserviceDbContext.SaveChanges();
                return "true";
            }
            catch (Exception e)
            {

                throw e;

            }
        }

        public List<Property> DisplayAllProperties()
        {
            try
            {
                return(_propertyMicroserviceDbContext.Properties.ToList());

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        
        public List<Property> DisplayAllPropertiesByType(string Type)
        {
            try
            {
                return _propertyMicroserviceDbContext.Properties.Where(p => p.PropertyType == Type).ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<Property> DisplayAllPropertiesByLocality(string Locality)
        {
            try
            {
                return _propertyMicroserviceDbContext.Properties.Where(p => p.Locality == Locality).ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Property DisplayPropertyById(int propertyId)
        {
            try
            {
                return _propertyMicroserviceDbContext.Properties.SingleOrDefault(p => p.PropertyId==propertyId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

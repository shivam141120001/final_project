using PropertyMicroservice.Models;
using PropertyMicroservice.Models.Auth;
using System.Collections.Generic;

namespace PropertyMicroservice.Repository
{
    public interface IPropertyRepository
    {
        public string AddProperty(Property property);

        public List<Property> DisplayAllProperties();

        public List<Property> DisplayAllPropertiesByType(string Type);

        public List<Property> DisplayAllPropertiesByLocality(string Locality);

        public Property DisplayPropertyById(int propertyId);
    }
}

using ManagerMicroservice.Models;
using ManagerMicroservice.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerMicroservice.Repository
{
    public interface IManagerRepository
    {
        public string AddExecutive(Executive executive);
        public List<Executive> DisplayAllExecutives();
        public List<Executive> DisplayAllExecutivesByLocality(string locality);
        public bool DisplayCustomerById(int customerId, TokenString token, out Customer customer);
        public bool DisplayAllCustomers(TokenString token, out IEnumerable<Customer> customers);
        public Executive GetExecutiveById(int executiveId);
        public bool AddProperty(Property property, TokenString token);
        public bool AssignExecutiveToCustomer(AssignExecutive assignExecutive, TokenString token);
        public bool getAllCustomersByExecutive(int executiveId, TokenString token, out IEnumerable<Customer> customers);
        public bool DisplayPropertyByType(string PropertyType, TokenString token, out IEnumerable<Property> properties);
        public bool DisplayPropertyByLocality(string Locality, TokenString token, out IEnumerable<Property> properties);
        public bool ManagerExists(User user, out AuthUser authUser);
        public bool ExecutiveExists(User user, out AuthUser authUser);
        public AuthToken GetToken(User user);
        public bool DisplayPropertyById(int PropertyId, TokenString token, out Property property);

    }
}

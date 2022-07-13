using ManagerMicroservice.Context;
using ManagerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using ManagerMicroservice.Models.Auth;

namespace ManagerMicroservice.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly ManagerMicroserviceDbContext _managerMicroserviceDbContext;
        public ManagerRepository(ManagerMicroserviceDbContext managerMicroserviceDbContext)
        {
            _managerMicroserviceDbContext = managerMicroserviceDbContext;
        }
        public string AddExecutive(Executive executive)
        {
            try
            {
                if (_managerMicroserviceDbContext.Executives.Any(e => e.ExecutiveId == executive.ExecutiveId))
                {
                    return false.ToString();
                }
                _managerMicroserviceDbContext.Executives.Add(executive);
                _managerMicroserviceDbContext.SaveChanges();
                return true.ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public List<Executive> DisplayAllExecutives()
        {
            try
            {

                return _managerMicroserviceDbContext.Executives.ToList();

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<Executive> DisplayAllExecutivesByLocality(string locality)
        {
            try
            {
                return _managerMicroserviceDbContext.Executives.Where(e => e.Locality == locality).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DisplayCustomerById(int customerId, TokenString token, out Customer customer)
        {
            try
            {
                customer = null;
                HttpResponseMessage response = new HttpResponseMessage();
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44366/api/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = client.GetAsync("Customer/getCustomerDetails?customerId=" + customerId).Result;
                    customer = JsonConvert.DeserializeObject<Customer>(response.Content.ReadAsStringAsync().Result);
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool DisplayAllCustomers(TokenString token, out IEnumerable<Customer> customers)
        {
            try
            {
                customers = null;
                HttpResponseMessage response = new HttpResponseMessage();
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient())
                {
                    
                    client.BaseAddress = new Uri("https://localhost:44366/api/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = client.GetAsync("Customer/displayAllCustomers").Result;

                    if ((int)response.StatusCode != 200) return false;

                    customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(response.Content.ReadAsStringAsync().Result);
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public Executive GetExecutiveById(int executiveId)
        {
            try
            {
                return _managerMicroserviceDbContext.Executives.SingleOrDefault(e => e.ExecutiveId == executiveId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool AddProperty(Property property, TokenString token)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44328/api/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    var stringProperty = JsonConvert.SerializeObject(property);
                    HttpContent c = new StringContent(stringProperty, Encoding.UTF8, "application/json");
                    response = client.PostAsync("Property/createProperty", c).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }


        }

        public bool AssignExecutiveToCustomer(AssignExecutive assignExecutive,TokenString token)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44366/api/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    var stringProperty = JsonConvert.SerializeObject(assignExecutive);
                    HttpContent c = new StringContent(stringProperty, Encoding.UTF8, "application/json");
                    response = client.PutAsync("Customer/assignExecutive", c).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool getAllCustomersByExecutive(int executiveId, TokenString token, out IEnumerable<Customer> customers)
        {
            try
            {
                customers = null;
                HttpResponseMessage response = new HttpResponseMessage();
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44366/api/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    
                    

                    response = client.GetAsync("Customer/getAllCustomersByExecutive?executiveId=" + executiveId).Result;
                    if ((int)response.StatusCode != 200) return false;

                    customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(response.Content.ReadAsStringAsync().Result);
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool DisplayPropertyByType(string PropertyType, TokenString token, out IEnumerable<Property> properties)
        {
            try
            {
                properties = null;
                HttpResponseMessage response = new HttpResponseMessage();
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44328/api/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    
                   

                    response = client.GetAsync("Property/PropertyType?Type=" + PropertyType).Result;
                    if ((int)response.StatusCode != 200) return false;

                    properties = JsonConvert.DeserializeObject<IEnumerable<Property>>(response.Content.ReadAsStringAsync().Result);
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool DisplayPropertyByLocality(string Locality, TokenString token, out IEnumerable<Property> properties)
        {
            try
            {
                properties = null;
                HttpResponseMessage response = new HttpResponseMessage();
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44328/api/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    response = client.GetAsync("Property/locality?Locality=" + Locality ).Result;
                    if ((int)response.StatusCode != 200) return false;

                    properties = JsonConvert.DeserializeObject<IEnumerable<Property>>(response.Content.ReadAsStringAsync().Result);
                    return true;
                    
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public AuthToken GetToken(User user)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                HttpClientHandler clientHandler = new HttpClientHandler();
                HttpContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = client.PostAsync("https://localhost:44322/api/auth/jwt", content).Result;
                    if ((int)response.StatusCode != 200) throw new Exception("Didn't got the token");
                    return JsonConvert.DeserializeObject<AuthToken>(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ManagerExists(User user, out AuthUser authUser)
        {
            try
            {
                authUser = null;
                Manager manager = _managerMicroserviceDbContext.Managers.Where(c => c.Username == user.Username && c.Password == user.Password).FirstOrDefault();
                if (manager == null) return false;
                authUser = new AuthUser
                {
                    Id = manager.ManagerId,
                    Role = Role.Manager
                };
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ExecutiveExists(User user, out AuthUser authUser)
        {
            try
            {
                authUser = null;
                Executive executive = _managerMicroserviceDbContext.Executives.Where(c => c.Username == user.Username && c.Password == user.Password).FirstOrDefault();
                if (executive == null) return false;
                authUser = new AuthUser
                {
                    Id = executive.ExecutiveId,
                    Role = Role.Executive
                };
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DisplayPropertyById(int propertyId, TokenString token, out Property property)
        {
            try
            {
                property = null;
                HttpResponseMessage response = new HttpResponseMessage();
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44328/api/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = client.GetAsync("Property/displayPropertyById?propertyId=" + propertyId).Result;

                    if ((int)response.StatusCode != 200) throw new Exception("Property not fetched");

                    property = JsonConvert.DeserializeObject<Property>(response.Content.ReadAsStringAsync().Result);
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

using ManagerMicroservice.Models;
using ManagerMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ManagerMicroservice.Models.Auth;
using ManagerMicroservice.Middleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManagerMicroservice.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _managerRepository;
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ManagerController));
        private readonly IAuthMiddleware _authMiddleware;
        
        public ManagerController( IManagerRepository managerRepository, IAuthMiddleware authMiddleware)
        {
            _managerRepository = managerRepository;
            _authMiddleware = authMiddleware;
        }
        
        private bool TryExtractToken(out TokenString tokenString)
        {
            try
            {
                //_log.Info("TryExtractToken: Process Initiated");

                tokenString = null;

                if (!HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    //_log.Info("TryExtractToken: Process Terminated With Message: Request headers does not contain Authorization Token.");
                    return false;
                }
                tokenString = new TokenString
                {
                    Token = ((string)HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value).Split().Last()
                };

                //_log.Info("TryExtractToken: Process Terminated Successfully.");
                return true;
            }
            catch (Exception exception)
            {
                //_log.Error("TryExtractToken: Process Terminated With Exception -", exception);
                throw;
            }
        }

        private bool TryHandleRequestAuth(string methodName, out AuthUser authUser)
        {
            try
            {
                //_log.Info("TryHandleRequestAuth: Process Initiated");

                authUser = null;

                if (!TryExtractToken(out TokenString tokenString))
                {
                    //_log.Info("TryHandleRequestAuth: Process Terminated With Message: Token can not be extracted.");
                    return false;
                }

                if (!_authMiddleware.ValidateToken(tokenString, out authUser))
                {
                    //_log.Info("TryHandleRequestAuth: Process Terminated With Message: Invalid Token.");
                    return false;
                }

                if (!_authMiddleware.IsRoleAuthorized(methodName, authUser.Role))
                {
                    //_log.Info("TryHandleRequestAuth: Process Terminated With Message: Unauthorized Role.");
                    return false;
                }

                //_log.Info("TryHandleRequestAuth: Process Terminated Successfully.");
                return true;
            }
            catch (Exception exception)
            {
                //_log.Error("TryHandleRequestAuth: Process Terminated With Exception -", exception);
                throw;
            }
        }

        
        // POST api/Manager/createExecutive
        [HttpPost]
        [Route("createExecutive")]
        public IActionResult CreateExecutive([FromBody] Executive executive)
        {
            try
            {
                _log.Info("CreateExecutive : Process Initiated");

                if (!TryHandleRequestAuth(ControllerMethods.CREATE_EXECUTIVE, out AuthUser authUser))
                {
                    _log.Info("CreateExecutive : Process Terminated With Message: Unauthorized Access.");
                    return Unauthorized();
                }
                var message = _managerRepository.AddExecutive(executive);
                if (message == true.ToString())
                {
                    _log.Info("CreateExecutive : Process Terminated Successfully.");
                    return Ok();
                }

                _log.Info("CreateExecutive : Executive Credential Already Exists.");
                return Conflict();
            }
            catch (Exception exception)
            {
                _log.Error("CreateExecutive: Process Terminated With Exception -", exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        //GET : https://localhost:44348/api/Manager/getAllExecutives
        [HttpGet]
        [Route("getAllExecutives")]
        public IActionResult GetAllExecutives()
        {
            try
            {
                _log.Info("GetAllEXecutives : Process Initiated");

                if (!TryHandleRequestAuth(ControllerMethods.GET_ALL_EXECUTIVES, out AuthUser authUser))
                {
                    _log.Info("GetAllExecutives: Process Terminated With Message: Unauthorized Access.");
                    return Unauthorized();
                }
                _log.Info("GetAllExecutives: Process Terminated Successfully.");
                return  Ok(_managerRepository.DisplayAllExecutives());
            }
            catch (Exception exception)
            {
                _log.Error("GetAllEXecutives : Process Terminated With Exception -", exception);
                return NoContent();
            }
        }

        
        // GET : https://localhost:44348/api/Manager/getAllExecutivesByLocality
        [HttpGet]
        [Route("getAllExecutivesByLocality")]
        public IActionResult GetAllExecutivesByLocality([FromQuery] string locality )
        {
            try
            {
                _log.Info("GetAllEXecutivesByLocality : Process Initiated");

                if (!TryHandleRequestAuth(ControllerMethods.GET_ALL_EXECUTIVES_BY_LOCALITY, out AuthUser authUser))
                {
                    _log.Info("GetAllExecutivesByLocality: Process Terminated With Message: Unauthorized Access.");
                    return Unauthorized();
                }
                _log.Info("GetAllExecutivesByLocality: Process Terminated Successfully.");
                return Ok(_managerRepository.DisplayAllExecutivesByLocality(locality));

            }
            catch (Exception exception)
            {
                _log.Error("GetAllEXecutivesByLocality : Process Terminated With Exception -", exception);
                return NoContent();
            }
        }

        
        // GET : https://localhost:44348/api/Manager/getCustomersById?customerId=1
        [HttpGet]
        [Route("getCustomersById")]
        public IActionResult GetCustomersById([FromQuery] int customerId)
        {
            try
            {
                _log.Info("GetCustomersById : Process Initiated");

                if (!TryHandleRequestAuth(ControllerMethods.GET_CUSTOMER_BY_ID , out AuthUser authUser))
                { 
                    _log.Info("GetCustomerById : Process Terminated With Message: Unauthorized Access.");
                    return Unauthorized();
                }

                TryExtractToken(out TokenString token);

                if (!_managerRepository.DisplayCustomerById(customerId,token, out Customer customer))
                {
                    _log.Info("GetCustomerById : Process Terminated With Message: Customers not fetched.");
                    return NoContent();
                }
                _log.Info("GetCustomerById: Process Terminated Successfully.");
                return Ok(customer);
            }
            catch (Exception exception)
            {
                _log.Error("GetCustomersById : Process Terminated With Exception -", exception);
                return NotFound();
            }
        }

        
        //Get : https://localhost:44348/api/Manager/getAllCustomers
        [HttpGet]
        [Route("getAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            try
            {
                _log.Info("GetAllCustomers : Process Initiated");

                if (!TryHandleRequestAuth(ControllerMethods.GET_ALL_CUSTOMERS, out AuthUser authUser))
                {
                    _log.Info("DisplayAllCustomers : Process Terminated With Message: Unauthorized Access.");
                    return Unauthorized();
                }

                TryExtractToken(out TokenString token);

                if (!_managerRepository.DisplayAllCustomers(token, out IEnumerable<Customer> customers))
                {
                    _log.Info("DisplayAllCustomers : Process Terminated With Message: Customers not fetched.");
                    return NoContent();
                }

                _log.Info("GetAllCustomers : Process Terminated Successfully.");
                return Ok(customers);
            }
            catch (Exception exception)
            {
                _log.Error("GetAllCustomers : Process Terminated With Exception -", exception);
                return NotFound();
            }
        }

        
        //Get : https://localhost:44348/api/Manager/getExecutiveById?executiveId=1
        [HttpGet]
        [Route("getExecutiveById")]
        public IActionResult GetExecutiveById([FromQuery] int executiveId)
        {
            try
            {
                _log.Info("GetExecutiveById : Process Initiated");

                if (!TryHandleRequestAuth(ControllerMethods.GET_EXECUTIVE_BY_ID, out AuthUser authUser))
                {
                    _log.Info("GetExecutiveById : Process Terminated With Message: Unauthorized Access.");
                    return Unauthorized();
                }

                _log.Info("GetExecutiveById : Process Terminated Successfully.");
                return Ok(_managerRepository.GetExecutiveById(executiveId));
            }
            catch (Exception exception)
            {
                _log.Error("GetExecutiveById : Process Terminated With Exception -", exception);
                return NotFound();
            }
        }


        [HttpPost]
        [Route("createProperty")]
        public IActionResult CreateProperty([FromBody] Property property)
        {
            try
            {
                _log.Info("CreateProperty : Process Initiated");
                if (!TryHandleRequestAuth(ControllerMethods.CREATE_PROPERTY, out AuthUser authUser))
                {
                    _log.Info("CreateProperty : Process Terminated With Message: Unauthorized Access.");
                    return Unauthorized();
                }
                TryExtractToken(out TokenString token);

                if (!_managerRepository.AddProperty(property, token))
                {
                    _log.Info("CreateProperty : Process Terminated With Message: Property not created. ");
                    return NoContent();
                }

                _log.Info("CreateProperty : Property Could not be Created.");
                return Ok();
            }
            catch (Exception exception)
            {
                _log.Error("CreateProperty : Process Terminated With Exception -", exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPut]
        [Route("assignExecutive")]
        public IActionResult AssignExecutive([FromBody] AssignExecutive assignExecutive)
        {
            try
            {
                _log.Info("AssignExecutive : Process Initiated");
                if(!TryHandleRequestAuth(ControllerMethods.ASSIGN_EXECUTIVE, out AuthUser authUser))
                {
                    _log.Info("AssignExecutive : Process Terminated With Message: Unauthorized Access.");
                    return Unauthorized();
                }
                TryExtractToken(out TokenString token);

                if (!_managerRepository.AssignExecutiveToCustomer(assignExecutive, token))
                {
                    _log.Info("AssignExecutive : Process Terminated With Message: Executive not assigned to customer. ");
                    return NoContent();
                }
   

                _log.Info("AssignExecutive : Executive could not be assigned.");
                return Ok();
            }
            catch (Exception exception)
            {
                _log.Error("AssignExecutive : Process Terminated With Exception -" , exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet]
        [Route("getAllCustomersByExecutive")]
        public IActionResult GetAllCustomersByExecutive([FromQuery] int executiveId)
        {
            try
            {
                _log.Info("GetAllCustomersByExecutive : Process Initiated");

                if (!TryHandleRequestAuth(ControllerMethods.GET_ALL_CUSTOMERS_BY_EXECUTIVE, out AuthUser authUser))
                {
                    _log.Info("GetAllCustomersByExecutive: Process Terminated With Message: Unauthorized Access.");
                    return Unauthorized();
                }

                TryExtractToken(out TokenString token);

                if (!_managerRepository.getAllCustomersByExecutive(executiveId, token, out IEnumerable<Customer> customers))
                {
                    _log.Info("GetAllCustomersByExecutive: Process Terminated With Message: Customers not fetched.");
                    return NoContent();
                }
                
                _log.Info("GetAllCustomersByExecutive : Process Terminated Successfully.");
                return Ok(customers);

            }
            catch (Exception exception)
            {

                _log.Error("GetAllCustomersByExecutive : Process Terminated With Exception -" , exception);
                return NoContent();

            }
        }


        [HttpGet]
        [Route("propertyByType")]
        public IActionResult DisplayPropertyByType([FromQuery] string Type)
        {
            try
            {
                _log.Info("DisplayPropertyByType : Process Initiated");

                if (!TryHandleRequestAuth(ControllerMethods.DISPLAY_PROPERTY_BY_TYPE, out AuthUser authUser))
                {
                    _log.Info("DisplayPropertyByType : Process Terminated With Message: Unauthorized Access.");
                    return Unauthorized();
                }

                TryExtractToken(out TokenString token);

                if (!_managerRepository.DisplayPropertyByType(Type, token, out IEnumerable<Property> properties))
                {
                    _log.Info("DisplayPropertyByType: Process Terminated With Message: Properties not fetched.");
                    return NoContent();
                }

                _log.Info("DisplayPropertyByType : Process Terminated Successfully.");
                return Ok(properties);
            }
            catch (Exception exception)
            {

                _log.Error("DisplayPropertyByType : Process Terminated With Exception -", exception);
                return NoContent();
            }
        }

        
        // GET: https://localhost:44348/api/Manager/
        [HttpGet]
        [Route("propertyByLocality")]
        public IActionResult DisplayPropertyByLocality([FromQuery] string Locality)
        {
            try
            {
                _log.Info("DisplayPropertyByLocality : Process Initiated");

                if (!TryHandleRequestAuth(ControllerMethods.DISPLAY_PROPERTY_BY_LOCALITY, out AuthUser authUser))
                {
                    _log.Info("DisplayPropertyByLocality : Process Terminated With Message: Unauthorized Access.");
                    return Unauthorized();
                }

                TryExtractToken(out TokenString token);

                if (!_managerRepository.DisplayPropertyByLocality(Locality,token, out IEnumerable<Property> properties))
                {
                    _log.Info("DisplayPropertyByLocality: Process Terminated With Message: Properties not fetched.");
                    return NoContent();
                }

                _log.Info("DisplayPropertyByLocality : Process Terminated Successfully.");
                return Ok(properties);
            }
            catch (Exception exception)
            {
                _log.Error("DisplayPropertyByLocality : Process Terminated With Exception -", exception);
                return NoContent();
            }
        }
        
        
        //POST: https://localhost:44348/api/Manager/managerLogin
        [HttpPost]
        [Route("managerLogin")]
        public IActionResult ManagerLogin([FromBody] User user)
        {
            try
            {
                _log.Info("ManagerLogin : Process Initiated");

                if (!Role.Exists(user.Role))
                {
                    _log.Info("ManagerLogin : Process Terminated With Message: Invalid Role.");
                    return Unauthorized();
                }

                if (!_managerRepository.ManagerExists(user, out AuthUser authUser))
                {
                    _log.Info("ManagerLogin : Process Terminated With Message: Invalid User Credentials.");
                    return Unauthorized();
                }

                _log.Info("ManagerLogin : Process Terminated Successfully.");
                return Ok(_managerRepository.GetToken(user));
            }
            catch (Exception exception)
            {
                _log.Error("ManagerLogin : Process Terminated With Exception -", exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        
        //POST: https://localhost:44348/api/Manager/executiveLogin
        [HttpPost]
        [Route("executiveLogin")]
        public IActionResult ExecutiveLogin([FromBody] User user)
        {
            try
            {
                _log.Info("ExecutiveLogin : Process Initiated");

                if (!Role.Exists(user.Role))
                {
                    _log.Info("ExecutiveLogin : Process Terminated With Message: Invalid Role.");
                    return Unauthorized();
                }

                if (!_managerRepository.ExecutiveExists(user, out AuthUser authUser))
                {
                    _log.Info("ExecutiveLogin : Process Terminated With Message: Invalid User Credentials.");
                    return Unauthorized();
                }

                _log.Info("ExecutiveLogin : Process Terminated Successfully.");
                return Ok(_managerRepository.GetToken(user));
            }
            catch (Exception exception)
            {
                _log.Error("ExecutiveLogin : Process Terminated With Exception -", exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        //POST: https://localhost:44348/api/Manager/validateManager
        [HttpPost]
        [Route("validateManager")]
        public IActionResult ValidateManager(User user)
        {
            try
            {
                _log.Info("ValidateManager : Process Initiated");

                if (!_managerRepository.ManagerExists(user, out AuthUser authUser))
                {
                    _log.Info("ValidateManager : Process Terminated With Message: Invalid User Credentials.");
                    return Unauthorized();
                }

                _log.Info("ValidateManager : Process Terminated Successfully.");
                return Ok(authUser);
            }
            catch (Exception exception)
            {
                _log.Error("ValidateManager : Process Terminated With Exception -", exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        
        //POST: https://localhost:44348/api/Manager/validateExecutive
        [HttpPost]
        [Route("validateExecutive")]
        public IActionResult ValidateExecutive(User user)
        {
            try
            {
                _log.Info("ValidateExecutive : Process Initiated");

                if (!_managerRepository.ExecutiveExists(user, out AuthUser authUser))
                {
                    _log.Info("ValidateExecutive : Process Terminated With Message: Invalid User Credentials.");
                    return Unauthorized();
                }

                _log.Info("ValidateExecutive : Process Terminated Successfully.");
                return Ok(authUser);
            }
            catch (Exception exception)
            {
                _log.Error("ValidateExecutive : Process Terminated With Exception -", exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        
        // GET: https://localhost:44348/api/Manager/displayPropertyById
        [HttpGet]
        [Route("displayPropertyById")]
        public IActionResult DisplayPropertyById([FromQuery] int propertyId)
        {
            try
            {
                _log.Info("DisplayPropertyById : Process Initiated");

                if (!TryHandleRequestAuth(ControllerMethods.DISPLAY_PROPERTY_BY_ID, out AuthUser authuser))
                {
                    _log.Info("DisplayPropertyById : Process Terminated With Message: Invalid User Credentials.");
                    return Unauthorized();
                }

                TryExtractToken(out TokenString token);

                if (!_managerRepository.DisplayPropertyById(propertyId, token , out Property property))
                {
                    _log.Info("DisplayPropertyById: Process Terminated With Message: Properties not fetched.");
                    return NoContent();
                }

                _log.Info("DisplayPropertyById : Process Terminated Successfully.");
                return Ok( property);
            }
            catch (Exception exception)
            {

                _log.Error("DisplayPropertyById : Process Terminated With Exception -", exception);
                return NoContent();
            }
        }

    }
}

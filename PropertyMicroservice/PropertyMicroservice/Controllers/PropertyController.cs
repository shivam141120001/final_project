using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PropertyMicroservice.Models;
using PropertyMicroservice;
using System.Linq;
using PropertyMicroservice.Repository;
using Microsoft.AspNetCore.Http;
using System;
using PropertyMicroservice.Models.Auth;
using PropertyMicroservice.Middleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PropertyMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IAuthMiddleware _authMiddleware;
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(PropertyController));
        public PropertyController(IPropertyRepository propertyRepository, IAuthMiddleware authMiddleware) 
        { 
            _propertyRepository=propertyRepository;
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

        // POST: https://localhost:44328/api/createProperty
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
                var message = _propertyRepository.AddProperty(property);
                if (message == "true")
                {
                    _log.Info("CreateProperty : Process Terminated Successfully.");
                    return Ok();
                }
                    _log.Info("CreateProperty : Duplicate Entry.");
                    return Conflict();
            }
            catch (Exception exception)
            {
                _log.Error("CreateProperty : Process Terminated With Exception -", exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            

        }

        // GET: https://localhost:44328/api/Property/allProperties
        [HttpGet]
        [Route("allProperties")]
        public IActionResult GetAllProperties()
        {
            try
            {
                _log.Info("GetAllProperties : Process Initiated");

                if (!TryHandleRequestAuth(ControllerMethods.GET_ALL_PROPERTIES, out AuthUser authUser))
                {
                    _log.Info("GetAllProperties : Process Terminated With Message: Unauthorized Access.");
                    return Unauthorized();
                }
                _log.Info("GetAllProperties : Process Terminated Successfully.");
                return Ok(_propertyRepository.DisplayAllProperties());
            }
            catch (Exception exception)
            {

                _log.Error("GetAllProperties : Process Terminated With Exception -", exception);
                return NoContent();
            }
        }


        // GETT: https://localhost:44328/api/Property/PropertyType
        [HttpGet]
        [Route("PropertyType")]

        public IActionResult getAllPropertiesByType([FromQuery] string Type)
        {
            try
            {
                _log.Info("getAllPropertiesByType : Process Initiated");

                if (!TryHandleRequestAuth(ControllerMethods.GET_ALL_PROPERTIES_BY_TYPE, out AuthUser authUser))
                {
                    _log.Info("getAllPropertiesByType  : Process Terminated With Message: Unauthorized Access.");
                    return Unauthorized();
                }
                _log.Info("getAllPropertiesByType : Process Terminated Successfully.");
                return Ok(_propertyRepository.DisplayAllPropertiesByType(Type));
            }
            catch (Exception exception)
            {

                _log.Error("getAllPropertiesByType : Process Terminated With Exception -", exception);
                return NoContent();
            }
        }

        // GETT: https://localhost:44328/api/Property/locality
        [HttpGet]
        [Route("locality")]

        public IActionResult getAllPropertiesByLocality([FromQuery] string Locality)
        {
            try
            {
                _log.Info("getAllPropertiesByLocality : Process Initiated");

                if (!TryHandleRequestAuth(ControllerMethods.GET_ALL_PROPERTIES_BY_LOCALITY, out AuthUser authUser))
                {
                    _log.Info("getAllPropertiesByLocality : Process Terminated With Message: Unauthorized Access.");
                    return Unauthorized();
                }
                _log.Info("getAllPropertiesByLocality : Process Terminated Successfully.");
                return Ok(_propertyRepository.DisplayAllPropertiesByLocality(Locality));
            }
            catch (Exception exception)
            {

                _log.Error("getAllPropertiesByLocality : Process Terminated With Exception -", exception);
                return NoContent();
            }
        }

        // GETT: https://localhost:44328/api/Property/displayPropertyById?propertyId=1
        [HttpGet]
        [Route("displayPropertyById")]

        public IActionResult DisplayPropertyById([FromQuery] int propertyId)
        {
            try
            {
                _log.Info("DisplayPropertyById : Process Initiated");

                //if (!TryHandleRequestAuth(ControllerMethods.DISPLAY_PROPERTY_BY_ID, out AuthUser authUser))
                //{
                //    _log.Info("DisplayPropertyById : Process Terminated With Message: Unauthorized Access.");
                //    return Unauthorized();
                //}
                _log.Info("DisplayPropertyById : Process Terminated Successfully.");
                return Ok(_propertyRepository.DisplayPropertyById(propertyId));
            }
            catch (Exception exception)
            {

                _log.Error("DisplayPropertyById : Process Terminated With Exception -", exception);
                return NoContent();
            }
        }

    }
}

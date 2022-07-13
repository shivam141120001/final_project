using AuthenticationMicroservice.Models;
using AuthenticationMicroservice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthenticationMicroservice.Controllers
{
    [Route("api/auth/jwt")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(AuthController));
        public AuthController(IAuthService authService) {
            _authService = authService;
        }
        // POST: https://localhost:44322/api/auth/jwt/
        [HttpPost]
        public IActionResult GetToken([FromBody] User user)
        {
            try
            {
                _log.Info("Token Generation initiated");
                if (!_authService.IsRoleValid(user.Role)) return BadRequest();
                _log.Info("Role verified");
                if (!_authService.IsUserValid(user, out AuthUser authUser)) return Unauthorized();
                _log.Info("User Verified");
                string token = _authService.GenerateToken(authUser);
                AuthToken authToken = new AuthToken() { User = authUser, Token = token };
                return Ok(authToken);
            }
            catch (Exception ex)
            {
                _log.Error("Generate Token Error: ", ex);
                return StatusCode(500);
            }
        }

        // POST: https://localhost:44322/api/auth/jwt/validate
        [HttpPost]
        [Route("validate")]
        public IActionResult Validate([FromBody] TokenString tokenObj)
        {
            try
            {
                AuthUser authUser = _authService.VerifyToken(tokenObj);
                return Ok(authUser);
            }
            catch (Exception)
            {
                return StatusCode(401);
            }
        }
    }
}

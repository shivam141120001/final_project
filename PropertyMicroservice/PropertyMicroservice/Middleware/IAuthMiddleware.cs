using PropertyMicroservice.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyMicroservice.Middleware
{
    public interface IAuthMiddleware
    {
        public bool ValidateToken(TokenString token, out AuthUser authUser);
        public bool IsRoleAuthorized(string methodName, string role);
    }
}

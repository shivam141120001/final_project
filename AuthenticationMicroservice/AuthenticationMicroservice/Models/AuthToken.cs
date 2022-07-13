using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationMicroservice.Models
{
    public class AuthToken
    {
        public AuthUser User { get; set; }
        public string Token { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationMicroservice.Models
{
    public class AuthUser
    {
        public int Id { get; set; }
        public string Role { get; set; }
    }
}

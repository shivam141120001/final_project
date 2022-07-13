using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerMicroservice.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string EmailId { get; set; }

        public long ContactNumber { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int ExecutiveId { get; set; }

    }
}

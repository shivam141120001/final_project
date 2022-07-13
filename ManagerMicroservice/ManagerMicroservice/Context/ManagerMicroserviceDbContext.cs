using ManagerMicroservice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerMicroservice.Context
{
    public class ManagerMicroserviceDbContext : DbContext
    {
        public ManagerMicroserviceDbContext(DbContextOptions options) : base(options)
        { 
        }
        public DbSet<Executive> Executives { get; set; }

        public DbSet<Manager> Managers { get; set; }
        
    }
}

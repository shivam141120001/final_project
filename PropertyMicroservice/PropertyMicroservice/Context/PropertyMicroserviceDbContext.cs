using Microsoft.EntityFrameworkCore;
using PropertyMicroservice.Models;

namespace PropertyMicroservice.Context
{
    public class PropertyMicroserviceDbContext: DbContext
    {
        public PropertyMicroserviceDbContext(DbContextOptions options): base(options)
        {
        }
        public DbSet<Property> Properties { get; set; }
    }
}

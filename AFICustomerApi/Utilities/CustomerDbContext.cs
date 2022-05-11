using AFICustomerApi.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace AFICustomerApi.Utilities
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }
    }
}

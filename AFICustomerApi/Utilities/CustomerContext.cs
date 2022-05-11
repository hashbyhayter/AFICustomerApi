using AFICustomerApi.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace AFICustomerApi.Utilities
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }
    }
}

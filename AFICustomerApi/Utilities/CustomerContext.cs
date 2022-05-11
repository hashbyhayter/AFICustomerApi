namespace AFICustomerApi.Utilities
{
    using AFICustomerApi.Model.DTO;
    using Microsoft.EntityFrameworkCore;
    
    /// <summary>
    /// Database context for the customer database
    /// </summary>
    public class CustomerContext : DbContext
    {
        /// <summary>
        /// Customer records
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }
    }
}

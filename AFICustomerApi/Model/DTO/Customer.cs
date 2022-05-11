using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFICustomerApi.Model.DTO
{
    [Table("Customer")]
    public class Customer
    {
        [Column("Id")]
        public int CustomerId { get; set; }

        [Column("FirstName"), Required]
        public string? FirstName { get; set; }
        [Column("Surname"), Required]
        public string? Surname { get; set; }
        [Column("PolicyReference"), Required]
        public string? PolicyReference { get; set; }
        [Column("DateOfBirth")]
        public DateTime? DateOfBirth { get; set; }
        [Column("Email")]
        public string? Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace AFICustomerApi.Model
{
    /// <summary>
    /// request object for a customer
    /// </summary>
    public class Customer
    {
        [Required, MaxLength(50), MinLength(3)]
        public string? FirstName { get; set; }

        [Required, MaxLength(50), MinLength(3)]
        public string? Surname { get; set; }
        
        [Required, RegularExpression("^[A-Z]{2}-[0-9]{6}$")]
        public string? PolicyReference { get; set; }

        public DateTime? DateOfBirth { get; set; }
        
        public string? Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace AFICustomerApi.Model
{
    /// <summary>
    /// Request object for a customer.
    /// <remarks>
    /// Either date of birth or email must be set
    /// </remarks>
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Customer's first name
        /// </summary>
        [Required, MaxLength(50), MinLength(3)]
        public string? FirstName { get; set; }

        /// <summary>
        /// Customer's surname
        /// </summary>
        [Required, MaxLength(50), MinLength(3)]
        public string? Surname { get; set; }

        /// <summary>
        /// Customer's policy reference
        /// </summary>
        [Required, RegularExpression("^[A-Z]{2}-[0-9]{6}$")]
        public string? PolicyReference { get; set; }
        
        /// <summary>
        /// Customer's date of birth
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        
        /// <summary>
        /// Customer's email address
        /// </summary>
        public string? Email { get; set; }
    }
}

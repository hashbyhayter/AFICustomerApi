namespace AFICustomerApi.Model.DTO
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Customer record
    /// </summary>
    [Table("Customer")]
    public class Customer
    {
        /// <summary>
        /// The customer id
        /// </summary>
        [Column("Id")]
        public int CustomerId { get; set; }

        /// <summary>
        /// The customer's first name
        /// </summary>
        [Column("FirstName"), Required]
        public string? FirstName { get; set; }
        
        /// <summary>
        /// The customer's surname
        /// </summary>
        [Column("Surname"), Required]
        public string? Surname { get; set; }

        /// <summary>
        /// The customer's policy reference
        /// </summary>
        [Column("PolicyReference"), Required]
        public string? PolicyReference { get; set; }
        
        /// <summary>
        /// The customer's date of birth
        /// </summary>
        [Column("DateOfBirth")]
        public DateTime? DateOfBirth { get; set; }
        
        /// <summary>
        /// The customer's email address
        /// </summary>
        [Column("Email")]
        public string? Email { get; set; }
    }
}

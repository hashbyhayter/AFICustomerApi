namespace AFICustomerApi.Model.DTO
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PolicyReference { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
    }
}

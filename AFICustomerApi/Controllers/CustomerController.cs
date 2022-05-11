namespace AFICustomerApi.Controllers
{
    using AFICustomerApi.Model;
    using AFICustomerApi.Utilities;
    using AFICustomerApi.Validation;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    /// <summary>
    /// Controller for the customer endpoint
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IValidator<Customer> validator;
        private readonly CustomerContext db;

        /// <summary>
        /// Constructor for <see cref="CustomerController"/>
        /// </summary>
        /// <param name="validator">A validator for a customer record</param>
        /// <param name="db">The databas context</param>
        public CustomerController(IValidator<Customer> validator,
            CustomerContext db)
        {
            this.validator = validator;
            this.db = db;
        }

        /// <summary>
        /// Creates a new customer record
        /// </summary>
        /// <param name="value">Customer details</param>
        /// <returns>the inserted customer id</returns>
        [HttpPost]
        [SwaggerResponse(201, "Customer created", typeof(int))]
        [SwaggerResponse(400, "Validation failed", typeof(Object))]
        public ActionResult<int> Post([FromBody] Customer value)
        {
            ModelState.Merge(this.validator.Validate(value));
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var customer = this.db.Customers.Add(new Model.DTO.Customer
            {
                FirstName = value.FirstName,
                Surname = value.Surname,
                PolicyReference = value.PolicyReference,
                DateOfBirth = value.DateOfBirth?.Date,
                Email = value.Email,
            });

            this.db.SaveChanges();

            return Created(string.Format("api/customer/{0}", customer.Entity.CustomerId), customer.Entity.CustomerId);
        }
    }
}

namespace AFICustomerApi.Controllers
{
    using AFICustomerApi.Model;
    using AFICustomerApi.Utilities;
    using AFICustomerApi.Validation;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IValidator<Customer> validator;
        private readonly CustomerDbContext db;

        public CustomerController(IValidator<Customer> validator,
            CustomerDbContext db)
        {
            this.validator = validator;
            this.db = db;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CustomerController>
        [HttpPost]
        public ActionResult<int> Post([FromBody] Customer value)
        {
            ModelState.Merge(this.validator.Validate(value));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = this.db.Customers.Add(new Model.DTO.Customer
            {
                FirstName = value.FirstName,
                Surname = value.Surname,
                PolicyReference = value.PolicyReference,
                DateOfBirth = value.DateOfBirth,
                Email = value.Email,
            });

            this.db.SaveChanges();

            return customer.Entity.CustomerId;
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}

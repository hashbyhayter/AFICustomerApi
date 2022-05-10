namespace AFICustomerApi.Controllers
{
    using System.Text.RegularExpressions;
    using AFICustomerApi.Model;
    using AFICustomerApi.Utilities;
    using AFICustomerApi.Validation;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IValidator<Customer> validator;

        public CustomerController(IValidator<Customer> validator)
        {
            this.validator = validator;   
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

            // datebase insert here
            return 1;
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}

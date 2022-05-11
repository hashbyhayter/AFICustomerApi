namespace AFICustomerApiTests.Controllers
{
    using AFICustomerApi.Controllers;
    using AFICustomerApi.Model;
    using AFICustomerApi.Utilities;
    using AFICustomerApi.Validation;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.EntityFrameworkCore;
    using Moq;

    internal class CustomerControllerTests
    {
        Mock<IValidator<Customer>> validatorMock;
        CustomerContext dbContext;
        CustomerController controller;

        [SetUp]
        public void Setup()
        {
            validatorMock = new Mock<IValidator<Customer>>();
            validatorMock.Setup(x => x.Validate(It.IsAny<Customer>()))
                .Returns(new ModelStateDictionary());

            var options = new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase(databaseName: "testingDatabase")
                .Options;

            this.dbContext = new CustomerContext(options);

            controller = new CustomerController(validatorMock.Object, this.dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            if (this.dbContext is CustomerContext db)
            {
                db.Database.EnsureDeleted();
            }
        }

        [Test]
        public void TestPost_WhenValidationPasses_ThenTheRecordIsAdded()
        {
            var model = new Customer()
            {
                FirstName = "Hugh",
                Surname = "Ashby-Hayter",
                PolicyReference = "XX-999999",
                Email = "test@test.co.uk"
            };

            var actual = this.controller.Post(model);

            Assert.That(actual.Value, Is.EqualTo(0));
            Assert.That(this.dbContext.Customers.Count(), Is.EqualTo(1));
            
            var record = this.dbContext.Customers.First();
            
            Assert.That(record.FirstName, Is.EqualTo("Hugh"));
            Assert.That(record.Surname, Is.EqualTo("Ashby-Hayter"));
            Assert.That(record.PolicyReference, Is.EqualTo("XX-999999"));
            Assert.That(record.Email, Is.EqualTo("test@test.co.uk"));
            Assert.That(record.DateOfBirth, Is.Null);
        }

        [Test]
        public void TestPost_WhenValidationFails_ThenNothingIsAddedToTheDatabase()
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("email", "error");

            validatorMock.Setup(x => x.Validate(It.IsAny<Customer>()))
                .Returns(modelState);

            var model = new Customer()
            {
                FirstName = "Hugh",
                Surname = "Ashby-Hayter",
                PolicyReference = "XX-999999",
                Email = "test@test.co.uk"
            };

            var actual = this.controller.Post(model);

            Assert.That(actual.Value, Is.EqualTo(0));
            Assert.That(this.dbContext.Customers.Count(), Is.EqualTo(0));
        }
    }
}

namespace AFICustomerApiTests.Validator
{
    using AFICustomerApi.Utilities;
    using AFICustomerApi.Validation;
    using Moq;
    internal class CustomerValidatorTests
    {
        Mock<IDateTimeService> dataTimeMock;
        CustomerValidator validator;

        [SetUp]
        public void Setup()
        {
            dataTimeMock = new Mock<IDateTimeService>();
            dataTimeMock.Setup(x => x.Today).Returns(new DateTime(2022, 05, 10));

            validator = new CustomerValidator(dataTimeMock.Object);
        }

        [Test]
        [TestCase("test@test.co.uk", true)]
        [TestCase("test@test.com", true)]
        [TestCase("t3st@test12.com", true)]
        [TestCase("test@test.gov.uk", false)]
        [TestCase("te-st@test.co.uk", false)]
        [TestCase("test@test.cooom", false)]
        [TestCase("test@t.co.uk", false)]
        [TestCase("tes@test.co.uk", false)]
        [TestCase("test@test.co.uk@test.co.uk", false)]
        [TestCase("test@test@test.co.uk", false)]
        [TestCase("testest.co.uk", false)]
        public void TestEmailFormat(string email, bool expected)
        {
            var model = new AFICustomerApi.Model.Customer()
            {
                FirstName = "Hugh",
                Surname = "Ashby-Hayter",
                PolicyReference = "XX-999999",
                Email = email,
            };
            
            var actual = this.validator.Validate(model);
            Assert.That(actual.IsValid, Is.EqualTo(expected));
            
            if (!expected)
            {
                Assert.That(actual.ContainsKey("email"), Is.True);
                Assert.That(actual["email"]?.Errors[0].ErrorMessage, Is.EqualTo("Email is an invalid format"));
            }
        }

        [Test]
        [TestCase("2002-01-01", true)]
        [TestCase("2004-05-09", true)]
        [TestCase("2004-05-10", true)]
        [TestCase("2004-05-11", false)]
        [TestCase("2004-05-13", false)]
        [TestCase("2012-01-01", false)]
        [TestCase("2022-05-10", false)]
        public void TestDateOfBirthAgeCheck(DateTime dob, bool expected)
        {
            var model = new AFICustomerApi.Model.Customer()
            {
                FirstName = "Hugh",
                Surname = "Ashby-Hayter",
                PolicyReference = "XX-999999",
                DateOfBirth = dob,
            };

            var actual = this.validator.Validate(model);
            Assert.That(actual.IsValid, Is.EqualTo(expected));

            if (!expected)
            {
                Assert.That(actual.ContainsKey("DateOfBirth"), Is.True);
                Assert.That(actual["DateOfBirth"]?.Errors[0].ErrorMessage, Is.EqualTo("Customer must be at least 18 years old"));
            }
        }

        [Test]
        public void TestCannotSetEmailAndDateOfBirth()
        {
            var model = new AFICustomerApi.Model.Customer()
            {
                FirstName = "Hugh",
                Surname = "Ashby-Hayter",
                PolicyReference = "XX-999999",
                DateOfBirth = new DateTime(2002, 01, 01),
                Email = "test@test.co.uk",
            };

            var actual = this.validator.Validate(model);
            Assert.That(actual.IsValid, Is.False);
            Assert.That(actual.ContainsKey("Email"), Is.True);
            Assert.That(actual["Email"]?.Errors[0].ErrorMessage, Is.EqualTo("Customer email and date of birth can not be both used"));
        }

        [Test]
        public void TestEmailAndDateOfBirthMustBeSet()
        {
            var model = new AFICustomerApi.Model.Customer()
            {
                FirstName = "Hugh",
                Surname = "Ashby-Hayter",
                PolicyReference = "XX-999999",
            };

            var actual = this.validator.Validate(model);
            Assert.That(actual.IsValid, Is.False);
            Assert.That(actual.ContainsKey("Email"), Is.True);
            Assert.That(actual["Email"]?.Errors[0].ErrorMessage, Is.EqualTo("Either customer email or date of birth must be set"));
        }
    }
}

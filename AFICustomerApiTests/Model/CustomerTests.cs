namespace AFICustomerApiTests.Model
{
    using System.ComponentModel.DataAnnotations;
    internal class CustomerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("XX-999999", true)]
        [TestCase("AB-123456", true)]
        [TestCase("xX-999999", false)]
        [TestCase("X-99999", false)]
        [TestCase("XX-9999991", false)]
        [TestCase("9X-999999", false)]
        [TestCase("9X9-99999", false)]
        [TestCase("9X-99A999", false)]
        [TestCase("9a-99A999", false)]
        public void TestPolicyReference(string reference, bool expected)
        {
            var result = new List<ValidationResult>();
            var model = new AFICustomerApi.Model.Customer()
            {
                FirstName = "Hugh",
                Surname = "Ashby-Hayter",
                PolicyReference = reference
            };

            var actual = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(model, new ValidationContext(model), result, true);

            Assert.That(actual, Is.EqualTo(expected));
            if (!expected)
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0].MemberNames.ElementAt(0), Is.EqualTo("PolicyReference"));
            }
        }

        [Test]
        [TestCase("Hugh", true)]
        [TestCase("Lorem ipsum dolor sit amet, consectetur adipiscing", true)]
        [TestCase("Joe", true)]
        [TestCase("Jo", false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("Lorem ipsum dolor sit amet, consectetur adipiscing.", false)]
        [TestCase("", false)]
        public void TestFirstname(string firstName, bool expected)
        {
            var result = new List<ValidationResult>();
            var model = new AFICustomerApi.Model.Customer()
            {
                FirstName = firstName,
                Surname = "Ashby-Hayter",
                PolicyReference = "XX-999999"
            };

            var actual = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(model, new ValidationContext(model), result, true);

            Assert.That(actual, Is.EqualTo(expected));
            if (!expected)
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0].MemberNames.ElementAt(0), Is.EqualTo("FirstName"));
            }
        }

        [Test]
        [TestCase("Hugh", true)]
        [TestCase("Lorem ipsum dolor sit amet, consectetur adipiscing", true)]
        [TestCase("Joe", true)]
        [TestCase("Jo", false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("Lorem ipsum dolor sit amet, consectetur adipiscing.", false)]
        [TestCase("", false)]
        public void TestSurname(string surname, bool expected)
        {
            var result = new List<ValidationResult>();
            var model = new AFICustomerApi.Model.Customer()
            {
                FirstName = "Hugh",
                Surname = surname,
                PolicyReference = "XX-999999"
            };

            var actual = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(model, new ValidationContext(model), result, true);

            Assert.That(actual, Is.EqualTo(expected));
            if (!expected)
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0].MemberNames.ElementAt(0), Is.EqualTo("Surname"));
            }
        }
    }
}

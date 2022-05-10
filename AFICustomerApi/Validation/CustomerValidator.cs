using System.Text.RegularExpressions;
using AFICustomerApi.Model;
using AFICustomerApi.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AFICustomerApi.Validation
{
    public class CustomerValidator : IValidator<Customer>
    {
        private readonly IDateTimeService dateTimeService;

        public CustomerValidator(IDateTimeService dateTimeService)
        {
            this.dateTimeService = dateTimeService;
        }

        public ModelStateDictionary Validate(Customer value)
        {
            var state = new ModelStateDictionary();
            if (value.DateOfBirth.HasValue && !string.IsNullOrEmpty(value.Email))
            {
                state.AddModelError("Email", "Customer email and date of birth can not be both used");
                return state;
            }

            if (!value.DateOfBirth.HasValue && string.IsNullOrEmpty(value.Email))
            {
                state.AddModelError("Email", "Either customer email or date of birth must be set");
                return state;
            }

            if (value.DateOfBirth.HasValue && this.dateTimeService.Today.AddYears(-18) < value.DateOfBirth)
            {
                state.AddModelError("DateOfBirth", "Customer must be at least 18 years old");
                return state;
            }

            if (!string.IsNullOrEmpty(value.Email))
            {
                if (!value.Email.Contains('@'))
                {
                    state.AddModelError("Email", "Email is an invalid format");
                    return state;
                }

                var emailParts = value.Email.Split('@');
                if (emailParts.Length != 2)
                {
                    state.AddModelError("Email", "Email is an invalid format");
                    return state;
                }

                var firstPattern = new Regex("^[a-zA-Z0-9]{4,}$");
                var secondPattern = new Regex("^[a-zA-Z0-9]{2,}.co(m|.uk)$");

                if (!firstPattern.IsMatch(emailParts[0]) || !secondPattern.IsMatch(emailParts[1]))
                {
                    state.AddModelError("Email", "Email is an invalid format");
                    return state;
                }
            }

            return state;
        }
    }
}

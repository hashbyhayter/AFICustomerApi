using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AFICustomerApi.Validation
{
    public interface IValidator<T>
    {
        ModelStateDictionary Validate(T value);
    }
}

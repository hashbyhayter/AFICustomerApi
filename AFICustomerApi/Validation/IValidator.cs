namespace AFICustomerApi.Validation
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    public interface IValidator<T>
    {
        /// <summary>
        /// Validates that the request object <typeparamref name="T"/> has no errors
        /// </summary>
        /// <param name="value">The request object</param>
        /// <returns>a collection of validation errors if there are any</returns>
        ModelStateDictionary Validate(T value);
    }
}

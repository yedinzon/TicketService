using FluentValidation.Results;

namespace TicketApi.Helpers.ValidationHelper;


public interface IValidationService
{
    /// <summary>
    /// Validates a model using the appropriate validator.
    /// </summary>
    /// <typeparam name="T">The type of the model to validate.</typeparam>
    /// <param name="model">The model instance to validate.</param>
    /// <returns></returns>
    Task<ValidationResult> ValidateAsync<T>(T model);
}

using FluentValidation.Results;

namespace TicketApi.Helpers.ValidationHelper;


public interface IValidationService
{
    Task<ValidationResult> ValidateAsync<T>(T model);
}

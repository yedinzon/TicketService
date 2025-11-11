using FluentValidation;
using FluentValidation.Results;

namespace TicketApi.Helpers.ValidationHelper;

public class ValidationService : IValidationService
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Validates a model using the appropriate validator.
    /// </summary>
    /// <typeparam name="T">The type of the model to validate.</typeparam>
    /// <param name="model">The model instance to validate.</param>
    /// <returns></returns>
    public async Task<ValidationResult> ValidateAsync<T>(T model)
    {
        var validator = _serviceProvider.GetService<IValidator<T>>();
        if (validator == null) return new ValidationResult();

        return await validator.ValidateAsync(model);
    }
}

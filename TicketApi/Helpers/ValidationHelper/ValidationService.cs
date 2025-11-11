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

    public async Task<ValidationResult> ValidateAsync<T>(T model)
    {
        var validator = _serviceProvider.GetService<IValidator<T>>();
        if (validator == null) return new ValidationResult();

        return await validator.ValidateAsync(model);
    }
}

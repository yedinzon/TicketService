using FluentValidation;
using TicketApi.Models.Requests;

namespace TicketApi.Validators;

public class CreateTicketRequestValidator : AbstractValidator<CreateTicketRequest>
{
    /// <summary>
    /// Validator for CreateTicketRequest
    /// </summary>
    public CreateTicketRequestValidator()
    {
        RuleFor(x => x.Usuario)
            .NotEmpty().WithMessage("Nombre de usuario es requerido")
            .MaximumLength(100).WithMessage("El nombre de usuario no puede exceder 100 caracteres.");
    }
}

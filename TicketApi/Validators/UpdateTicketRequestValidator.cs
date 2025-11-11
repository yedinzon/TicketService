using FluentValidation;
using TicketApi.Helpers;
using TicketApi.Models.Requests;

namespace TicketApi.Validators;

public class UpdateTicketRequestValidator : AbstractValidator<UpdateTicketRequest>
{
    public UpdateTicketRequestValidator()
    {
        RuleFor(x => x.Usuario)
            .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre de usuario no puede exceder 100 caracteres.");

        RuleFor(x => x.Estado)
            .Must(EnumValidators.BeAValidTicketStatus)
            .WithMessage("El estado debe ser alguna de las siguiente opciones: 'Open', 'Closed'.");
    }
}

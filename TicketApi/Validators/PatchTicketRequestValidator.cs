using FluentValidation;
using TicketApi.Helpers;
using TicketApi.Models.Requests;

namespace TicketApi.Validators;

public class PatchTicketRequestValidator : AbstractValidator<PatchTicketRequest>
{
    public PatchTicketRequestValidator()
    {
        RuleFor(x => x.Usuario)
            .MaximumLength(100).WithMessage("El nombre de usuario no puede exceder 100 caracteres.");

        RuleFor(x => x.Estado)
            .Must(status => status == null || EnumValidators.BeAValidTicketStatus(status))
            .WithMessage("El estado debe ser alguna de las siguiente opciones: 'Open', 'Closed'.");
    }
}

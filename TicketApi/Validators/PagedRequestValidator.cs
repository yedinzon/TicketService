using FluentValidation;
using TicketApi.Models.Requests;

namespace TicketApi.Validators;

public class PagedRequestValidator : AbstractValidator<PagedRequest>
{
    /// <summary>
    /// Validator for PagedRequest
    /// </summary>
    public PagedRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("El número de página debe ser mayor que 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("El tamaño de página debe ser mayor que 0")
            .LessThanOrEqualTo(100).WithMessage("El tamaño de página no debe ser mayor que 100");
    }
}

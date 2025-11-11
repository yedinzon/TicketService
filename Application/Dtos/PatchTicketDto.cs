using Domain.Enums;

namespace Application.Dtos;

public record PatchTicketDto
{
    public string? Username { get; set; }
    public TicketStatus? Status { get; set; }
}

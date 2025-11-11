using Domain.Enums;

namespace Application.Dtos;

/// <summary>
/// DTO for patching a ticket.
/// </summary>
public record PatchTicketDto
{
    public string? Username { get; set; }
    public TicketStatus? Status { get; set; }
}

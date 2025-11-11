using Domain.Enums;

namespace Application.Dtos;

/// <summary>
/// DTO for updating a Ticket
/// </summary>
public record UpdateTicketDto
{
    public string Username { get; set; } = null!;
    public TicketStatus Status { get; set; }
}

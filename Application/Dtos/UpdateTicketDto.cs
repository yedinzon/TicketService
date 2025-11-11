using Domain.Enums;

namespace Application.Dtos;

public record UpdateTicketDto
{
    public string Username { get; set; } = null!;
    public TicketStatus Status { get; set; }
}

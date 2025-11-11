using Domain.Enums;

namespace Application.Dtos;

/// <summary>
/// DTO for Ticket entity
/// </summary>
public record TicketDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public TicketStatus Status { get; set; }
}

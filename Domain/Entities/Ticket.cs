using Domain.Enums;

namespace Domain.Entities;

public class Ticket
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Username { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; } = null;
    public TicketStatus Status { get; private set; } = TicketStatus.Open;
}

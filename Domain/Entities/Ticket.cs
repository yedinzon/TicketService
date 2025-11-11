using Domain.Enums;

namespace Domain.Entities;

public class Ticket
{
    public Guid Id { get; private set; }
    public string Username { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public TicketStatus Status { get; private set; }

    public Ticket(string username)
    {
        Id = Guid.NewGuid();
        Username = username;
        CreatedAt = DateTime.UtcNow;
        Status = TicketStatus.Open;
    }
    private Ticket() { }
}

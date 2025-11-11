using Domain.Enums;

namespace Domain.Entities;

public class Ticket
{
    public Guid Id { get; private set; }
    public string Username { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public TicketStatus Status { get; private set; }

    /// <summary>
    /// Creates a new ticket with the specified username.
    /// </summary>
    /// <param name="username">The username associated with the ticket.</param>
    public Ticket(string username)
    {
        Id = Guid.NewGuid();
        Username = username;
        CreatedAt = DateTime.UtcNow;
        Status = TicketStatus.Open;
    }

    /// <summary>
    /// Change the username associated with the ticket.
    /// </summary>
    /// <param name="newUsername">The new username.</param>
    public void ChangeUsername(string newUsername)
    {
        Username = newUsername;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Change the status of the ticket.
    /// </summary>
    /// <param name="newStatus">The new status.</param>
    public void ChangeStatus(TicketStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// To prevent parameterless instantiation from outside the class.
    /// </summary>
    private Ticket() { }
}

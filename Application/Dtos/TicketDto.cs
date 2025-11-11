using Domain.Enums;

namespace Application.Dtos;

public class TicketDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public TicketStatus Status { get; set; }
}

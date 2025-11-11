namespace Application.Dtos;

/// <summary>
/// DTO for creating a ticket
/// </summary>
public record CreateTicketDto
{
    public string Username { get; set; } = null!;
}

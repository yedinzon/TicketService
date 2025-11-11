namespace TicketApi.Models.Requests;

/// <summary>
/// Request model for creating a ticket
/// </summary>
public record CreateTicketRequest
{
    public string Usuario { get; set; } = null!;
}

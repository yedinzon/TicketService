namespace TicketApi.Models.Requests;

/// <summary>
/// Request model for updating a ticket
/// </summary>
public record UpdateTicketRequest
{
    public string Usuario { get; set; } = null!;
    public string Estado { get; set; } = null!;
}

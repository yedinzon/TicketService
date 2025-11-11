namespace TicketApi.Models.Requests;

/// <summary>
/// Request model for patching a ticket
/// </summary>
public record PatchTicketRequest
{
    public string? Usuario { get; set; }
    public string? Estado { get; set; }
}

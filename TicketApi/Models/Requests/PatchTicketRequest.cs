namespace TicketApi.Models.Requests;

public record PatchTicketRequest
{
    public string? Usuario { get; set; }
    public string? Estado { get; set; }
}

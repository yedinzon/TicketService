namespace TicketApi.Models.Requests;

public record UpdateTicketRequest
{
    public string Usuario { get; set; } = null!;
    public string Estado { get; set; } = null!;
}

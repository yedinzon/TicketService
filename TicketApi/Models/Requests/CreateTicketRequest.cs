namespace TicketApi.Models.Requests;

public record CreateTicketRequest
{
    public string Usuario { get; set; } = null!;
}

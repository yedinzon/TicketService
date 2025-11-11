namespace TicketApi.Models.Responses;

public record TicketResponse
{
    public Guid Codigo { get; set; }
    public string Usuario { get; set; } = null!;
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    public string Estado { get; set; } = null!;
}

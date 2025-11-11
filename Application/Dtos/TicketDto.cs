namespace Application.Dtos;

public class TicketDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Estatus { get; set; }
}

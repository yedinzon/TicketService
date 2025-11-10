using Domain.Entities;

namespace Application.Interfaces.Services;

public interface ITicketService
{
    Task<IEnumerable<Ticket>> GetAll();
}

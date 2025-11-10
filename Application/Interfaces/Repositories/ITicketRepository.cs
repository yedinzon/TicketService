using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ITicketRepository
{
    Task<IEnumerable<Ticket>> GetAll();
}

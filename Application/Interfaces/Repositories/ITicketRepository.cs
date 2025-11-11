using Application.Common;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ITicketRepository
{
    Task<IEnumerable<Ticket>> GetAll();
    Task<PagedResult<Ticket>> GetPagedAsync(int pageNumber, int pageSize);
}

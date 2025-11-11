using Application.Common;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ITicketRepository
{
    Task<IEnumerable<Ticket>> GetAllAsync();
    Task<Ticket?> GetByIdAsync(Guid id);
    Task<PagedResult<Ticket>> GetPagedAsync(int pageNumber, int pageSize);
}

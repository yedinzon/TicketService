using Application.Common;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ITicketRepository
{
    Task<IEnumerable<Ticket>> GetAllAsync();
    Task<PagedResult<Ticket>> GetPagedAsync(int pageNumber, int pageSize);
    Task<Ticket?> GetByIdAsync(Guid id);
    Task<Ticket> CreateAsync(Ticket ticket);
    Task DeleteAsync(Ticket ticket);
}

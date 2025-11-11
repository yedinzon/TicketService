using Application.Common;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for Ticket entity,
/// defines methods for CRUD operations and pagination.
/// </summary>
public interface ITicketRepository
{
    Task<IEnumerable<Ticket>> GetAllAsync();
    Task<PagedResult<Ticket>> GetPagedAsync(int pageNumber, int pageSize);
    Task<Ticket?> GetByIdAsync(Guid id);
    Task<Ticket> CreateAsync(Ticket ticket);
    Task<Ticket> UpdateAsync(Ticket ticket);
    Task DeleteAsync(Ticket ticket);
}

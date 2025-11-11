using Application.Common;
using Application.Dtos;

namespace Application.Interfaces.Services;

public interface ITicketService
{
    Task<IEnumerable<TicketDto>> GetAllAsync();
    Task<PagedResult<TicketDto>> GetPagedAsync(int pageNumber, int pageSize);
    Task<TicketDto?> GetByIdAsync(Guid id);
    Task<TicketDto> CreateAsync(CreateTicketDto createTicketDto);
    Task<bool> DeleteAsync(Guid id);
}

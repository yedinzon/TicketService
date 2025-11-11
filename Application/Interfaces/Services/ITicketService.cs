using Application.Common;
using Application.Dtos;

namespace Application.Interfaces.Services;

public interface ITicketService
{
    Task<TicketDto> CreateAsync(CreateTicketDto createTicketDto);
    Task<IEnumerable<TicketDto>> GetAllAsync();
    Task<TicketDto?> GetByIdAsync(Guid id);
    Task<PagedResult<TicketDto>> GetPagedAsync(int pageNumber, int pageSize);
}

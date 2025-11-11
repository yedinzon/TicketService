using Application.Common;
using Application.Dtos;

namespace Application.Interfaces.Services;

public interface ITicketService
{
    Task<IEnumerable<TicketDto>> GetAllAsync();
    Task<PagedResult<TicketDto>> GetPagedAsync(int pageNumber, int pageSize);
    Task<TicketDto?> GetByIdAsync(Guid id);
    Task<TicketDto> CreateAsync(CreateTicketDto createTicketDto);
    Task<TicketDto?> UpdateAsync(Guid id, UpdateTicketDto updateTicket);
    Task<TicketDto?> PatchAsync(Guid id, PatchTicketDto patchTicket);
    Task<bool> DeleteAsync(Guid id);
}

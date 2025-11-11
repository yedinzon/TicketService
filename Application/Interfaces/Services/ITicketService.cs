using Application.Common;
using Application.Dtos;

namespace Application.Interfaces.Services;

public interface ITicketService
{
    Task<IEnumerable<TicketDto>> GetAll();
    Task<PagedResult<TicketDto>> GetPagedAsync(int pageNumber, int pageSize);
}

using Application.Common;
using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _repository;
    private readonly IMapper _mapper;

    public TicketService(ITicketRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<TicketDto>> GetAll()
    {
        var x = await _repository.GetAll();
        return _mapper.Map<IEnumerable<TicketDto>>(x);
    }

    public async Task<PagedResult<TicketDto>> GetPagedAsync(int pageNumber, int pageSize)
    {
        PagedResult<Ticket> pagedResult = await _repository.GetPagedAsync(pageNumber, pageSize);
        IReadOnlyList<TicketDto> mappedItems = _mapper.Map<IReadOnlyList<TicketDto>>(pagedResult.Items);

        return new PagedResult<TicketDto>(
            mappedItems,
            pagedResult.PageNumber,
            pagedResult.PageSize,
            pagedResult.TotalCount);
    }
}

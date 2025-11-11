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

    public async Task<IEnumerable<TicketDto>> GetAllAsync()
    {
        IEnumerable<Ticket> tickets = await _repository.GetAllAsync();

        return _mapper.Map<IEnumerable<TicketDto>>(tickets);
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

    public async Task<TicketDto?> GetByIdAsync(Guid id)
    {
        Ticket? ticket = await _repository.GetByIdAsync(id);

        return ticket is null
            ? null
            : _mapper.Map<TicketDto>(ticket);
    }

    public async Task<TicketDto> CreateAsync(CreateTicketDto createTicketDto)
    {
        var ticket = new Ticket(createTicketDto.Username);
        Ticket createdTicket = await _repository.CreateAsync(ticket);

        return _mapper.Map<TicketDto>(createdTicket);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Ticket? ticket = await _repository.GetByIdAsync(id);
        if (ticket is null) return false;
        await _repository.DeleteAsync(ticket);

        return true;
    }
}

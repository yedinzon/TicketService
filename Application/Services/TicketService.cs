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

    /// <summary>
    /// Gets all tickets.
    /// </summary>
    /// <returns>A collection of <see cref="TicketDto"/> representing all tickets.</returns>
    public async Task<IEnumerable<TicketDto>> GetAllAsync()
    {
        IEnumerable<Ticket> tickets = await _repository.GetAllAsync();

        return _mapper.Map<IEnumerable<TicketDto>>(tickets);
    }

    /// <summary>
    /// Gets a paged list of tickets.
    /// </summary>
    /// <param name="pageNumber">Number of the page to retrieve.</param>
    /// <param name="pageSize">Size of the page to retrieve.</param>
    /// <returns>A <see cref="PagedResult{T}"/> containing <see cref="TicketDto"/> items.</returns>
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

    /// <summary>
    /// Gets a ticket by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the ticket.</param>
    /// <returns>A <see cref="TicketDto"/> representing the ticket, or null if not found.</returns>
    public async Task<TicketDto?> GetByIdAsync(Guid id)
    {
        Ticket? ticket = await _repository.GetByIdAsync(id);

        return ticket is null
            ? null
            : _mapper.Map<TicketDto>(ticket);
    }

    /// <summary>
    /// Creates a new ticket.
    /// </summary>
    /// <param name="createTicket">The DTO containing ticket creation data.</param>
    /// <returns>A <see cref="TicketDto"/> representing the created ticket.</returns>
    public async Task<TicketDto> CreateAsync(CreateTicketDto createTicket)
    {
        var ticket = new Ticket(createTicket.Username);
        Ticket createdTicket = await _repository.CreateAsync(ticket);

        return _mapper.Map<TicketDto>(createdTicket);
    }

    /// <summary>
    /// Updates an existing ticket.
    /// </summary>
    /// <param name="id">The unique identifier of the ticket to update.</param>
    /// <param name="updateTicket">The DTO containing updated ticket data.</param>
    /// <returns>A <see cref="TicketDto"/> representing the updated ticket, or null if not found.</returns>
    public async Task<TicketDto?> UpdateAsync(Guid id, UpdateTicketDto updateTicket)
    {
        Ticket? ticket = await _repository.GetByIdAsync(id);
        if (ticket is null) return null;
        ticket.ChangeUsername(updateTicket.Username);
        ticket.ChangeStatus(updateTicket.Status);
        Ticket ticketUpdated = await _repository.UpdateAsync(ticket);

        return _mapper.Map<TicketDto>(ticketUpdated);
    }

    /// <summary>
    /// Partially updates an existing ticket.
    /// </summary>
    /// <param name="id">The unique identifier of the ticket to patch.</param>
    /// <param name="patchTicket">The DTO containing ticket data to patch.</param>
    /// <returns>A <see cref="TicketDto"/> representing the patched ticket, or null if not found.</returns>
    public async Task<TicketDto?> PatchAsync(Guid id, PatchTicketDto patchTicket)
    {
        Ticket? ticket = await _repository.GetByIdAsync(id);
        if (ticket is null) return null;

        if (!string.IsNullOrEmpty(patchTicket.Username) && patchTicket.Username != ticket.Username)
            ticket.ChangeUsername(patchTicket.Username);

        if (patchTicket.Status.HasValue && patchTicket.Status != ticket.Status)
            ticket.ChangeStatus(patchTicket.Status.Value);

        Ticket ticketUpdated = await _repository.UpdateAsync(ticket);

        return _mapper.Map<TicketDto>(ticketUpdated);
    }

    /// <summary>
    /// Deletes a ticket by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the ticket to delete.</param>
    /// <returns>True if the ticket was deleted; otherwise, false.</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        Ticket? ticket = await _repository.GetByIdAsync(id);
        if (ticket is null) return false;
        await _repository.DeleteAsync(ticket);

        return true;
    }
}

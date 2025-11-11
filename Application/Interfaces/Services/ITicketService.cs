using Application.Common;
using Application.Dtos;

namespace Application.Interfaces.Services;

public interface ITicketService
{
    /// <summary>
    /// Gets all tickets.
    /// </summary>
    /// <returns>A collection of <see cref="TicketDto"/> representing all tickets.</returns>
    Task<IEnumerable<TicketDto>> GetAllAsync();

    /// <summary>
    /// Gets a paged list of tickets.
    /// </summary>
    /// <param name="pageNumber">Number of the page to retrieve.</param>
    /// <param name="pageSize">Size of the page to retrieve.</param>
    /// <returns>A <see cref="PagedResult{T}"/> containing <see cref="TicketDto"/> items.</returns>
    Task<PagedResult<TicketDto>> GetPagedAsync(int pageNumber, int pageSize);

    /// <summary>
    /// Gets a ticket by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the ticket.</param>
    /// <returns>A <see cref="TicketDto"/> representing the ticket, or null if not found.</returns>
    Task<TicketDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Creates a new ticket.
    /// </summary>
    /// <param name="createTicket">The DTO containing ticket creation data.</param>
    /// <returns>A <see cref="TicketDto"/> representing the created ticket.</returns>
    Task<TicketDto> CreateAsync(CreateTicketDto createTicket);

    /// <summary>
    /// Updates an existing ticket.
    /// </summary>
    /// <param name="id">The unique identifier of the ticket to update.</param>
    /// <param name="updateTicket">The DTO containing updated ticket data.</param>
    /// <returns>A <see cref="TicketDto"/> representing the updated ticket, or null if not found.</returns>
    Task<TicketDto?> UpdateAsync(Guid id, UpdateTicketDto updateTicket);

    /// <summary>
    /// Partially updates an existing ticket.
    /// </summary>
    /// <param name="id">The unique identifier of the ticket to patch.</param>
    /// <param name="patchTicket">The DTO containing ticket data to patch.</param>
    /// <returns>A <see cref="TicketDto"/> representing the patched ticket, or null if not found.</returns>
    Task<TicketDto?> PatchAsync(Guid id, PatchTicketDto patchTicket);

    /// <summary>
    /// Deletes a ticket by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the ticket to delete.</param>
    /// <returns>True if the ticket was deleted; otherwise, false.</returns>
    Task<bool> DeleteAsync(Guid id);
}

using Application.Common;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly TicketDbContext _context;

    public TicketRepository(TicketDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        return await _context.Tickets
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<PagedResult<Ticket>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _context.Tickets
            .AsNoTracking()
            .OrderByDescending(t => t.CreatedAt)
            .ToPagedResultAsync(pageNumber, pageSize);
    }

    public async Task<Ticket?> GetByIdAsync(Guid id)
    {
        return await _context.Tickets
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Ticket> CreateAsync(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<Ticket> UpdateAsync(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task DeleteAsync(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
    }
}

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

    public async Task<IEnumerable<Ticket>> GetAll()
    {
        var x = await _context.Tickets.ToListAsync();

        return x;
    }

    public async Task<PagedResult<Ticket>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _context.Tickets.AsNoTracking()
           .OrderByDescending(t => t.CreatedAt)
           .ToPagedResultAsync(pageNumber, pageSize);
    }
}

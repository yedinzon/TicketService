using Application.Interfaces.Repositories;
using Domain.Entities;
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
}

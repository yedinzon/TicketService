using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _repository;

    public TicketService(ITicketRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<Ticket>> GetAll()
    {
        return await _repository.GetAll();
    }
}

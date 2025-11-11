using Application.Common;
using Application.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace TicketApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<TicketDto> tickets = await _ticketService.GetAllAsync();
            return Ok(tickets);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            PagedResult<TicketDto> ticketsPaged = await _ticketService.GetPagedAsync(pageNumber, pageSize);
            return Ok(ticketsPaged);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            TicketDto? raffle = await _ticketService.GetByIdAsync(id);
            if (raffle is null) return NotFound();
            return Ok(raffle);
        }
    }
}

//TODO: Validar parámetros de paginación y manejar errores.
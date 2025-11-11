using Application.Common;
using Application.Dtos;
using Application.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketApi.Models.Requests;

namespace TicketApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;

        public TicketController(ITicketService ticketService, IMapper mapper)
        {
            _ticketService = ticketService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequest request)
        {
            var ticket = _mapper.Map<CreateTicketDto>(request);
            TicketDto createdTicket = await _ticketService.CreateAsync(ticket);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdTicket.Id },
                createdTicket);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<TicketDto> tickets = await _ticketService.GetAllAsync();
            return Ok(tickets);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            TicketDto? raffle = await _ticketService.GetByIdAsync(id);
            if (raffle is null) return NotFound();
            return Ok(raffle);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            PagedResult<TicketDto> ticketsPaged = await _ticketService.GetPagedAsync(pageNumber, pageSize);
            return Ok(ticketsPaged);
        }
    }
}

//TODO: Validar parámetros de paginación y manejar errores.
//TODO: Fluentvalidations
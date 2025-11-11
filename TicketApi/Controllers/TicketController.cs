using Application.Common;
using Application.Dtos;
using Application.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketApi.Models.Requests;
using TicketApi.Models.Responses;

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
            var ticketsResponse = _mapper.Map<IEnumerable<TicketResponse>>(tickets);

            return Ok(ticketsResponse);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            TicketDto? ticket = await _ticketService.GetByIdAsync(id);
            if (ticket is null) return NotFound();
            var ticketResponse = _mapper.Map<TicketResponse>(ticket);

            return Ok(ticketResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            PagedResult<TicketDto> ticketsPaged = await _ticketService.GetPagedAsync(pageNumber, pageSize);

            var ticketsResponse = new PagedResult<TicketResponse>(
                items: _mapper.Map<IReadOnlyList<TicketResponse>>(ticketsPaged.Items),
                ticketsPaged.PageNumber,
                ticketsPaged.PageSize,
                ticketsPaged.TotalCount);

            return Ok(ticketsResponse);
        }
    }
}

//TODO: Validar parámetros de paginación y manejar errores.
//TODO: Fluentvalidations
using Application.Common;
using Application.Dtos;
using Application.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TicketApi.Models.Requests;
using TicketApi.Models.Responses;

namespace TicketApi.Controllers;

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

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<TicketDto> tickets = await _ticketService.GetAllAsync();

        return Ok(_mapper.Map<IEnumerable<TicketResponse>>(tickets));
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

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        TicketDto? ticket = await _ticketService.GetByIdAsync(id);
        if (ticket is null) return NotFound();

        return Ok(_mapper.Map<TicketResponse>(ticket));
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

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketRequest request)
    {
        var ticketToUpdate = _mapper.Map<UpdateTicketDto>(request);
        TicketDto? updatedTicket = await _ticketService.UpdateAsync(id, ticketToUpdate);
        if (updatedTicket is null) return NotFound();

        return Ok(_mapper.Map<TicketResponse>(updatedTicket));
    }

    [HttpPatch("{id:Guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        Guid id,
        [FromBody] JsonPatchDocument<PatchTicketRequest> patchDoc)
    {
        if (patchDoc is null) return BadRequest();

        var ticketRequest = new PatchTicketRequest();
        patchDoc.ApplyTo(ticketRequest);

        var ticketToPatch = _mapper.Map<PatchTicketDto>(ticketRequest);
        TicketDto? updatedTicket = await _ticketService.PatchAsync(id, ticketToPatch);
        if (updatedTicket == null) return NotFound();

        return Ok(_mapper.Map<TicketResponse>(updatedTicket));
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool deleted = await _ticketService.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}

//TODO: Validar parámetros de paginación y manejar errores.
//TODO: Fluentvalidations - enum en request
using Application.Common;
using Application.Dtos;
using Application.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TicketApi.Helpers.ValidationHelper;
using TicketApi.Models.Requests;
using TicketApi.Models.Responses;

namespace TicketApi.Controllers;

[ApiController]
[Route("tickets")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly IValidationService _validationService;
    private readonly IMapper _mapper;

    public TicketController(
        ITicketService ticketService,
        IValidationService validationService,
        IMapper mapper)
    {
        _ticketService = ticketService;
        _validationService = validationService;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtienes todos los tickets.
    /// </summary>
    /// <returns> 
    /// Una lista de tipo <see cref="TicketResponse"/> con todos los tickets.
    /// </returns>
    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<TicketResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<TicketDto> tickets = await _ticketService.GetAllAsync();

        return Ok(_mapper.Map<IEnumerable<TicketResponse>>(tickets));
    }

    /// <summary>
    /// Obtiene una lista paginada de tickets.
    /// </summary>
    /// <param name="pagedRequest">Los parámetros de paginación.</param>
    /// <returns>
    /// Una lista paginada <see cref="PagedResult{T}"/> de tipo <see cref="TicketResponse"/>.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<TicketResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPaged([FromQuery] PagedRequest pagedRequest)
    {
        var result = await _validationService.ValidateAsync(pagedRequest);
        if (!result.IsValid)
            return BadRequest(result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        PagedResult<TicketDto> ticketsPaged =
            await _ticketService.GetPagedAsync(pagedRequest.PageNumber, pagedRequest.PageSize);

        var ticketsResponse = new PagedResult<TicketResponse>(
            items: _mapper.Map<IReadOnlyList<TicketResponse>>(ticketsPaged.Items),
            ticketsPaged.PageNumber,
            ticketsPaged.PageSize,
            ticketsPaged.TotalCount);

        return Ok(ticketsResponse);
    }

    /// <summary>
    /// Obtiene un ticket por su identificador único (GUID).
    /// </summary>
    /// <param name="id">Identificador único del ticket a recuperar.</param>
    /// <returns>
    /// El ticket <see cref="TicketResponse"/> correspondiente al identificador proporcionado.
    /// </returns>
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(TicketResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        TicketDto? ticket = await _ticketService.GetByIdAsync(id);
        if (ticket is null) return NotFound();

        return Ok(_mapper.Map<TicketResponse>(ticket));
    }

    /// <summary>
    /// Crea un nuevo ticket.
    /// </summary>
    /// <param name="request">Los datos del ticket a crear.</param>
    /// <returns>El ticket creado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TicketResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateTicketRequest request)
    {
        var result = await _validationService.ValidateAsync(request);
        if (!result.IsValid)
            return BadRequest(result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var ticket = _mapper.Map<CreateTicketDto>(request);
        TicketDto createdTicket = await _ticketService.CreateAsync(ticket);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdTicket.Id },
            _mapper.Map<TicketResponse>(createdTicket));
    }

    /// <summary>
    /// Actualiza un ticket existente.
    /// </summary>
    /// <param name="id">Identificador único del ticket a actualizar.</param>
    /// <param name="request">Los datos del ticket a actualizar.</param>
    /// <returns>El ticket <see cref="TicketResponse"/> actualizado.</returns>
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(typeof(TicketResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketRequest request)
    {
        var result = await _validationService.ValidateAsync(request);
        if (!result.IsValid)
            return BadRequest(result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var ticketToUpdate = _mapper.Map<UpdateTicketDto>(request);
        TicketDto? updatedTicket = await _ticketService.UpdateAsync(id, ticketToUpdate);
        if (updatedTicket is null) return NotFound();

        return Ok(_mapper.Map<TicketResponse>(updatedTicket));
    }

    /// <summary>
    /// Actualiza parcialmente un ticket existente.
    /// </summary>
    /// <param name="id">Identificador único del ticket a actualizar.</param>
    /// <param name="patchDoc">Documento patch JSON que contiene las operaciones de actualización.</param>
    /// <returns>El ticket <see cref="TicketResponse"/> actualizado.</returns>
    [HttpPatch("{id:Guid}")]
    [Consumes("application/json-patch+json")]
    [ProducesResponseType(typeof(TicketResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Patch(
        Guid id,
        [FromBody] JsonPatchDocument<PatchTicketRequest> patchDoc)
    {
        var result = await _validationService.ValidateAsync(patchDoc);
        if (!result.IsValid)
            return BadRequest(result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        if (patchDoc is null) return BadRequest();

        var ticketRequest = new PatchTicketRequest();
        patchDoc.ApplyTo(ticketRequest);

        var ticketToPatch = _mapper.Map<PatchTicketDto>(ticketRequest);
        TicketDto? updatedTicket = await _ticketService.PatchAsync(id, ticketToPatch);
        if (updatedTicket == null) return NotFound();

        return Ok(_mapper.Map<TicketResponse>(updatedTicket));
    }

    /// <summary>
    /// Elimina un ticket por su identificador único (GUID).
    /// </summary>
    /// <param name="id">Identificador único del ticket a eliminar.</param>    
    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool deleted = await _ticketService.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _ticketService.GetAll();

            return Ok(result);
        }

        [HttpGet("GetPaged")]
        public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _ticketService.GetPagedAsync(pageNumber, pageSize);
            return Ok(result);
        }
    }
}

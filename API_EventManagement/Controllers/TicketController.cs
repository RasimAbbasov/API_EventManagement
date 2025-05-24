using API_EventManagement.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_EventManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController(EventAppDbContext eventAppDbContext) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var tickets = eventAppDbContext.Tickets.ToList();
            return Ok(tickets);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var ticketitem = eventAppDbContext.Tickets.FirstOrDefault(x => x.Id == id);
            if (ticketitem == null)
            {
                return NotFound();
            }
            return Ok(ticketitem);
        }

    }
}

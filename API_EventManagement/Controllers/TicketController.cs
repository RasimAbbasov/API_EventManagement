using API_EventManagement.Data;
using API_EventManagement.Dtos.Tickets;
using API_EventManagement.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_EventManagement.Controllers
{
    public class TicketController(EventAppDbContext eventAppDbContext,IMapper mapper) : BaseController
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
        [HttpPost] 
        public async  Task<IActionResult> Create([FromBody] TicketCreateDto ticketCreateDto)
        {
            if (eventAppDbContext.Tickets.Any(c => c.TicketType == ticketCreateDto.TicketType && c.EventId == ticketCreateDto.EventId))
            {
                return Conflict();
            }
            var ticketitem = mapper.Map<Ticket>(ticketCreateDto);
            eventAppDbContext.Tickets.Add(ticketitem);
            eventAppDbContext.SaveChanges();
            return Created();
        }

    }
}

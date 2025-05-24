using API_EventManagement.Data;
using API_EventManagement.Dtos.Organizers;
using API_EventManagement.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_EventManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerController(EventAppDbContext eventAppDbContext,IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var organizers = eventAppDbContext
                .Organizers
                .Include(x => x.Events)
                .ToList();
            var organizersReturnDto = mapper.Map<List<OrganizerReturnDto>>(organizers);
            return Ok(organizersReturnDto);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id) 
        {
         var organizer = eventAppDbContext.Organizers
                .Include(x=>x.Events)
                .FirstOrDefault(x => x.Id == id);
            if (organizer == null)
            {
                return NotFound();
            }
            var organizerReturnDto = mapper.Map<OrganizerReturnDto>(organizer);
            //var organizerReturnDto = new OrganizerReturnDto
            //{
            //    Name = organizer.Name,
            //    Email = organizer.Email,
            //    Phone = organizer.Phone,
            //    LogoUrl = organizer.LogoUrl,
            //    Events = organizer.Events.Select(e => new EventsBelongToOrganizer
            //    {
            //        Title = e.Title,
            //        Date = e.Date,
            //        Location = e.Location
            //    }).ToList()
            //};
         return Ok(organizerReturnDto);
        }
        [HttpPost]
        public IActionResult Add([FromForm]OrganizerCreateDto organizerCreateDto)
        {
            var organizer = new Organizer
            {
                Name = organizerCreateDto.Name,
                Email = organizerCreateDto.Email,
                Phone = organizerCreateDto.PhoneNumber
            };
            eventAppDbContext.Organizers.Add(organizer);
            eventAppDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}

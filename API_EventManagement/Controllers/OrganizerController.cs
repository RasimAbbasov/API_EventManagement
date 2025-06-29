using API_EventManagement.Data;
using API_EventManagement.Dtos.Events;
using API_EventManagement.Dtos.Organizers;
using API_EventManagement.Helpers;
using API_EventManagement.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_EventManagement.Controllers
{
    public class OrganizerController(EventAppDbContext eventAppDbContext,IMapper mapper) : BaseController
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
        [HttpPost]
        [Route("{id}/banner")]
        public IActionResult UploadBanner([FromRoute] int id, [FromForm] FileUploadDto dto)
        {

            var file = dto.File;
            if (file == null || file.Length == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else
            {
                if (!file.ContentType.Contains("image/") || file.Length > 2 * 1024 * 1024)
                {
                    return BadRequest();
                }

            }
            var existorganizer = eventAppDbContext.Organizers.FirstOrDefault(e => e.Id == id);
            if (existorganizer == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var organizerLogo = file.SaveImage("uploads");
            existorganizer.LogoUrl = organizerLogo;
            eventAppDbContext.SaveChanges();
            return Ok();
        }
    }
}

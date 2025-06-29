using API_EventManagement.Data;
using API_EventManagement.Dtos.Events;
using API_EventManagement.Helpers;
using API_EventManagement.Mappers;
using API_EventManagement.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_EventManagement.Controllers
{
    [Authorize]
    public class EventController(EventAppDbContext eventAppDbContext,IMapper mapper) : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            var events = eventAppDbContext.Events.ToList();
            return Ok(events);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var eventItem = eventAppDbContext.Events.FirstOrDefault(x => x.Id == id);
            if (eventItem == null)
            {
                return NotFound();
            }
            return Ok(eventItem);
        }
        [HttpPost]
        public IActionResult Add(EventCreateDto eventCreateDto)
        {
            if (eventAppDbContext.Events.Any(c => c.Title == eventCreateDto.Title))
            {
                return Conflict();
            }
            var eventItem =mapper.Map<Event>(eventCreateDto);
            eventAppDbContext.Events.Add(eventItem);
            eventAppDbContext.SaveChanges();
            return Created();
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute]int id,[FromBody] EventUpdateDto eventUpdateDto)
        {
            var eventItem = eventAppDbContext.Events.FirstOrDefault(x => x.Id == id);
            if (eventItem == null)
            {
                return NotFound();
            }
            if (eventAppDbContext.Events.Any(c => c.Title == eventUpdateDto.Title))
            {
                return Conflict();
            }
            //id-e gore problem olur
            //eventItem=EventMapper.UpdateDtoToEvent(eventUpdateDto);
            eventItem.OrganizerId = eventUpdateDto.OrganizerId;
            eventItem.Title = eventUpdateDto.Title;
            eventItem.Description = eventUpdateDto.Description;
            eventItem.Date = eventUpdateDto.Date;
            eventItem.Location = eventUpdateDto.Location; 
            eventItem.BannerImageUrl = eventUpdateDto.BannerImageUrl;

            eventAppDbContext.SaveChanges();
            return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var eventItem = eventAppDbContext.Events.FirstOrDefault(x => x.Id == id);
            if (eventItem == null)
            {
                return NotFound();
            }
            eventAppDbContext.Events.Remove(eventItem);
            eventAppDbContext.SaveChanges();
            return NoContent();
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
            var existevent = eventAppDbContext.Events.FirstOrDefault(e => e.Id == id);
            if (existevent == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var bannerimg = file.SaveImage("uploads");
            existevent.BannerImageUrl = bannerimg;
            eventAppDbContext.SaveChanges();
            return Ok();


        }
    }
}

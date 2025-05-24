using API_EventManagement.Dtos.Events;
using API_EventManagement.Models;

namespace API_EventManagement.Mappers
{
    public class EventMapper
    {
        public static Event CreateDtoToEvent(EventCreateDto eventCreateDto) =>
            new() 
            {
                Title = eventCreateDto.Title,
                Description = eventCreateDto.Description,
                Date = eventCreateDto.Date,
                Location = eventCreateDto.Location,
                OrganizerId = eventCreateDto.OrganizerId
            };
        public static Event UpdateDtoToEvent(EventUpdateDto eventUpdateDto) =>
            new()
            {   BannerImageUrl = eventUpdateDto.BannerImageUrl,
                Date = eventUpdateDto.Date,
                Description = eventUpdateDto.Description,
                Location = eventUpdateDto.Location,
                OrganizerId = eventUpdateDto.OrganizerId,
                Title = eventUpdateDto.Title
            };
    }
}

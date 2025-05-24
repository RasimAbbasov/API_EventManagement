using AutoMapper;
using API_EventManagement.Models;
using API_EventManagement.Dtos.Organizers;
using API_EventManagement.Dtos.Events;

namespace API_EventManagement.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile(IHttpContextAccessor httpContextAccessor)
        {
            var request = httpContextAccessor.HttpContext.Request;
            var uriBuilder = new UriBuilder(
                request.Scheme,
                request.Host.Host,
                (int)request.Host.Port
                );
            var url = uriBuilder.Uri.AbsoluteUri;
            CreateMap<EventCreateDto,Event> ();

            CreateMap<Organizer, OrganizerReturnDto>()
                .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => "localhost" + src.LogoUrl));
            CreateMap<Event, EventsinOrganizerReturnDto>();
            CreateMap<Event, EventReturnDto>().ForMember(dest => dest.BannerImageUrl, opt => opt.MapFrom(src => url + "uploads/" + src.BannerImageUrl));
            CreateMap<Event, EventsinOrganizerReturnDto>();
            CreateMap<Organizer,OrganizerInEventReturnDto>();
            CreateMap<OrganizerCreateDto, Organizer>();
        }
    }
}

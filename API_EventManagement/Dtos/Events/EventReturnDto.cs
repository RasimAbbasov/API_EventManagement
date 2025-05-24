using API_EventManagement.Models;

namespace API_EventManagement.Dtos.Events
{
    public class EventReturnDto
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string? BannerImageUrl { get; set; }
        public int OrganizerId { get; set; }
        public OrganizerInEventReturnDto Organizer { get; set; }
    }
    public class OrganizerInEventReturnDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}

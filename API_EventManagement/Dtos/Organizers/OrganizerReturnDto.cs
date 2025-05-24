using API_EventManagement.Models;

namespace API_EventManagement.Dtos.Organizers
{
    public class OrganizerReturnDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? LogoUrl { get; set; }
        public int EventsCount { get; set; }
        public List<EventsinOrganizerReturnDto> Events { get; set; }

    }
    public class EventsinOrganizerReturnDto
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }
}

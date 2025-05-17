namespace API_EventManagement.Models
{
    public class Organizer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? LogoUrl { get; set; }
        public List<Event> Events { get; set; }
    }
}

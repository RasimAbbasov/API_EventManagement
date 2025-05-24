namespace API_EventManagement.Dtos.Organizers
{
    public class OrganizerCreateDto
    {
        public string Name { get; set; }
        public IFormFile? File { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}

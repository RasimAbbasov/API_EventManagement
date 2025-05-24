﻿namespace API_EventManagement.Dtos.Events
{
    public class EventUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string? BannerImageUrl { get; set; }
        public int OrganizerId { get; set; }
    }
}

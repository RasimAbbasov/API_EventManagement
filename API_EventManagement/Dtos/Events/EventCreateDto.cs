﻿namespace API_EventManagement.Dtos.Events
{
    public class EventCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int OrganizerId { get; set; }
    }
}

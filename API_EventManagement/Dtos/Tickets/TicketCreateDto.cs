using API_EventManagement.Enums;
using API_EventManagement.Models;

namespace API_EventManagement.Dtos.Tickets
{
    public class TicketCreateDto
    {
        public int EventId { get; set; }
        public TicketType TicketType { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
    }
}

using API_EventManagement.Enums;

namespace API_EventManagement.Models
{
    public class Ticket:BaseEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        public TicketType TicketType { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
    }
}

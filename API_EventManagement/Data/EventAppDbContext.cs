using API_EventManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace API_EventManagement.Data
{
    public class EventAppDbContext:DbContext
    {
        public EventAppDbContext(DbContextOptions<EventAppDbContext> options) : base(options)
        {

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Organizer> Organizers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventAppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

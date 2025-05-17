using API_EventManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_EventManagement.Data.Configurations
{
    public class TicketConfiguration: IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Event)
                .WithMany(o => o.Tickets)
                .HasForeignKey(e => e.EventId);
            builder.Property(o => o.TicketType).IsRequired().HasMaxLength(50);
            builder.Property(o => o.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(o => o.QuantityAvailable).HasMaxLength(20);
        }
    }
}

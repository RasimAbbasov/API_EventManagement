using API_EventManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_EventManagement.Data.Configurations
{
    public class EventConfiguration:IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Organizer)
                .WithMany(o => o.Events)
                .HasForeignKey(e => e.OrganizerId);
            builder.Property(e => e.Title).IsRequired().HasMaxLength(150);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(500);
            builder.Property(e => e.Date).IsRequired();
            builder.Property(e => e.Location).IsRequired().HasMaxLength(200);
            builder.Property(e => e.BannerImageUrl).IsRequired();
            builder.Property(e => e.OrganizerId).IsRequired();
        }
    }
}

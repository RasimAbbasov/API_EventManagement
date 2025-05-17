using API_EventManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_EventManagement.Data.Configurations
{
    public class OrganizerConfiguration:IEntityTypeConfiguration<Organizer>
    {
        public void Configure(EntityTypeBuilder<Organizer> builder)
        {
            builder.ToTable("Organizers");
            builder.HasKey(o => o.Id);
            builder.HasMany(o => o.Events)
                .WithOne(e => e.Organizer)
                .HasForeignKey(e => e.OrganizerId);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(100);
            builder.Property(o => o.Email).IsRequired().HasMaxLength(100);
            builder.Property(o => o.Phone).HasMaxLength(20);
        }
    }
}

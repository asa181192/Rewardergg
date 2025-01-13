using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rewardergg.Domain;

namespace Rewardergg.Infrastructure.Configuration
{
    public class EventUserConfiguration : IEntityTypeConfiguration<EventUser>
    {
        public void Configure(EntityTypeBuilder<EventUser> builder)
        {
            builder.HasKey(e => new { e.EventId, e.UserId });

            builder.HasOne(e => e.User)
                .WithMany(s => s.EventParticipations)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Event)
                .WithMany(s => s.Participants)
                .HasForeignKey(e => e.EventId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rewardergg.Domain;

namespace Rewardergg.Infrastructure.Configuration
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasMany(w => w.Participants)
                .WithOne(w => w.Match)
                .HasForeignKey(w => w.MatchId);

            builder.HasOne(m => m.Event)
                .WithMany(e => e.Matches)
                .HasForeignKey(m => m.EventId);
        }
    }
}

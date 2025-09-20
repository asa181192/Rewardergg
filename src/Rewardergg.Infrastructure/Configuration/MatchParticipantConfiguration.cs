using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rewardergg.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Rewardergg.Infrastructure.Configuration
{
    public class MatchParticipantConfiguration : IEntityTypeConfiguration<MatchParticipant>
    {
        public void Configure(EntityTypeBuilder<MatchParticipant> builder)
        {
            builder.HasKey(mp => new { mp.MatchId, mp.UserId });

            builder.HasOne(mp => mp.Match)
                .WithMany(m => m.Participants)
                .HasForeignKey(mp => mp.MatchId);

            builder.HasOne(mp => mp.User)
                .WithMany() 
                .HasForeignKey(mp => mp.UserId);

        }
    }
}

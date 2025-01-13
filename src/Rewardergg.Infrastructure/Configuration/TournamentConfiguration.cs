using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rewardergg.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewardergg.Infrastructure.Configuration
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(e => e.Events)
                .WithOne(e => e.Tournament)
                .HasForeignKey(t => t.TournamentId);
        }
    }
}

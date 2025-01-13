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
    public class MatchResultConfiguration : IEntityTypeConfiguration<MatchResult>
    {
        public void Configure(EntityTypeBuilder<MatchResult> builder)
        {
            builder.HasOne(w => w.Winner)
                .WithMany()
                .HasForeignKey(w => w.WinnerId);

            builder.HasOne(w => w.Loser)
                .WithMany()
                .HasForeignKey(w => w.LoserId);
        }
    }
}

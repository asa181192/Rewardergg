using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rewardergg.Domain;
using System.Reflection.Emit;

namespace Rewardergg.Infrastructure.Configuration
{
    public class LeaderBoardConfiguration : IEntityTypeConfiguration<LeaderBoard>
    {
        public void Configure(EntityTypeBuilder<LeaderBoard> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(le => le.User)
                .WithMany()
                .HasForeignKey(le => le.UserId);
        }
    }
}

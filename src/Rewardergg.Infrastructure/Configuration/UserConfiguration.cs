using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Rewardergg.Domain;
using Rewardergg.Domain.Enums;
using System.Data;

namespace Rewardergg.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(u => u.Token)
                .WithOne(t => t.User)
                .HasForeignKey<UserToken>(ut => ut.UserId);

            // Configure Roles
            var rolesConverter = new ValueConverter<ICollection<string>, string>(
                roles => string.Join(",", roles),         // Serialize collection to a single string
                roles => roles.Split(',', StringSplitOptions.None).ToList() // Deserialize string back to collection
            );

            var rolesComparer = new ValueComparer<ICollection<string>>(
                (roles1, roles2) => roles1.SequenceEqual(roles2),          // Compare collections
                roles => roles.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), // HashCode for collection
                roles => roles.ToList()                                    // Deep copy the collection
            );

            builder.Property(u => u.Roles)
                .HasConversion(rolesConverter)        // Use the value converter
                .Metadata.SetValueComparer(rolesComparer); // Use the value comparer

        }
    }
}

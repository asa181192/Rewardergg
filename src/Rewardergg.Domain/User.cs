using Rewardergg.Domain.Common;
using Rewardergg.Domain.Enums;

namespace Rewardergg.Domain
{
    public class User : BaseDomainModel
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? GamerTag { get; set; }

        public string? Discriminator { get; set; }

        // Todo- Remove this property
        public string? DiscordId { get; set; }

        public int Points { get; set; }

        public bool IsActive { get; set; }

        public UserToken? Token { get; set; }

        public ICollection<EventUser>? EventParticipations { get; set; }

        public ICollection<string>? Roles { get; set; }
    }
}

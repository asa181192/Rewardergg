using Rewardergg.Domain.Common;

namespace Rewardergg.Domain
{
    public class User : BaseDomainModel
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? GamerTag { get; set; }

        public string? Discriminator { get; set; }

        public int UserId { get; set; }

        public int Points { get; set; }

        public bool IsActive { get; set; }

        public UserToken? Token { get; set; }

        public ICollection<EventUser>? EventParticipations { get; set; } = new List<EventUser>();

        public ICollection<string>? Roles { get; set; } = new List<string>();

        public DateTime? LastSyncedAt { get; set; }
    }
}

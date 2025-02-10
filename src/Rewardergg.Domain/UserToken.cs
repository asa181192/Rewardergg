using Rewardergg.Domain.Common;

namespace Rewardergg.Domain
{
    public class UserToken : BaseDomainModel
    {
        public Guid UserId { get; set; }

        public User? User { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public int ExpiresAt { get; set; }
    }
}

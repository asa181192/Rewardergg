using Rewardergg.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rewardergg.Domain
{
    public class UserToken : BaseDomainModel
    {
        public Guid UserId { get; set; }

        public User? User { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }

        public required string PlatformRefreshToken { get; set; }

        [Column(TypeName = "timestamptz")]
        public DateTimeOffset ExpiresAt { get; set; }
    }
}

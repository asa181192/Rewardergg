using Rewardergg.Domain.Common;

namespace Rewardergg.Domain
{
    public class UserEnrollment : BaseDomainModel
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    }
}

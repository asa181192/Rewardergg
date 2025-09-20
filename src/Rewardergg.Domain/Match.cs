using Rewardergg.Domain.Common;

namespace Rewardergg.Domain
{
    public class Match : BaseDomainModel
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public string Round { get; set; }          // e.g. pools, top 8, finals
        public DateTime PlayedAt { get; set; }  // when match happened

        // Navigation
        public ICollection<MatchParticipant> Participants { get; set; }
    }
}

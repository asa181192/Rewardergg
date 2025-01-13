using Rewardergg.Domain.Common;

namespace Rewardergg.Domain
{
    public class Event : BaseDomainModel
    {
        public required string Name { get; set; }

        public ICollection<EventUser>? Participants { get; set; }

        public Guid TournamentId { get; set; }

        public Tournament? Tournament { get; set; }

        public ICollection<MatchResult>? Sets { get; set; }

    }
}

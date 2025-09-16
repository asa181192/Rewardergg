using Rewardergg.Domain.Common;

namespace Rewardergg.Domain
{
    public class Tournament : BaseDomainModel
    {
        public required string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Category { get; set; }

        public string? Slug { get; set; } 

        public bool IsFinished { get; set; }

        public ICollection<Event>? Events { get; set; } = new List<Event>();


    }
}

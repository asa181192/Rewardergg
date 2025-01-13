using Rewardergg.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewardergg.Domain
{
    public class MatchResult : BaseDomainModel
    {
        public Guid EventId { get; set; }

        public Guid WinnerId { get; set; }

        public Guid LoserId { get; set; }

        public string?  Score { get; set; }

        // Relationships
        public Event? Event { get; set; }
        public User? Winner { get; set; }
        public User? Loser { get; set; }
    }
}

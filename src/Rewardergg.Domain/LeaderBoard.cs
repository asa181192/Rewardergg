using Rewardergg.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewardergg.Domain
{
    public class LeaderBoard : BaseDomainModel
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        // Leaderboard Metrics 
        public int Rank { get; set; }         // User's rank
        public int Points { get; set; }       // Total points earned
        public int Wins { get; set; }         // Total wins achieved
        public int Losses { get; set; }       // Total losses recorded
        public int MatchesPlayed { get; set; } // Total matches played
        public int EventsParticipated { get; set; } // Total events participated
    }
}

using Rewardergg.Domain.Common;

namespace Rewardergg.Domain
{
    public class EventUser 
    {
        public Guid EventId { get; set; } 
        public Event Event { get; set; } 

        public Guid UserId { get; set; }  
        public User User { get; set; }    

        public int TotalWins { get; set; } 
        public int TotalLosses { get; set; }    
        public int Placement { get; set; } 
        public int PointsEarned { get; set; } 

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    }
}

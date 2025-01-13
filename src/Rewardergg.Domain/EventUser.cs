namespace Rewardergg.Domain
{
    public class EventUser
    {
        public Guid EventId { get; set; } // Foreign key to Event
        public Event Event { get; set; }  // Navigation property

        public Guid UserId { get; set; }  // Foreign key to User
        public User User { get; set; }    // Navigation property

        public int TotalWins { get; set; } // User's total Wins in the event
        public int TotalLosses { get; set; }    // User's total losses in the event
        public int Placement { get; set; } // User's placement in the event
    }
}

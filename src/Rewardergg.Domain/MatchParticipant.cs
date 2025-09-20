namespace Rewardergg.Domain
{
    public class MatchParticipant
    {
        public Guid MatchId { get; set; }
        public Match Match { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public int Score { get; set; }      // e.g. 3 in a best-of-5
        public bool IsWinner { get; set; }  // flag for winner(s)

    }
}

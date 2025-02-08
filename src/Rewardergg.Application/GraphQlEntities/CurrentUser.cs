namespace Rewardergg.Application.GraphQlEntities
{
    public class Currentuser
    {
        public int id { get; set; }

        public string? name { get; set; }

        public string? discriminator { get; set; }

        public Player? player { get; set; }
        public string? email { get; set; }
    }
}

namespace Rewardergg.Application.GraphQlEntities
{
    public class Event
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isDisqualified { get; set; }

        public Standing? standing { get; set; }
    }
}

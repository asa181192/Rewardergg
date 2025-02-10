namespace Rewardergg.Application.Configurations
{
    public class StartggSettings
    {
        public int ClientId { get; set; }
        public required string ClientSecret { get; set; }
        public required string BaseUrl { get; set; }
        public required string GraphQlEndpoint { get; set; }
        public required string RedirectUrl { get; set; }
    }
}

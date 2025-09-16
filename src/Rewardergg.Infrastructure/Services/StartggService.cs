using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Rewardergg.Application.Configurations;
using Rewardergg.Application.DTOs;
using Rewardergg.Application.GraphQlEntities.Results;
using Rewardergg.Application.GraphQlQueries;
using Rewardergg.Application.Interfaces;
using System.Net.Http.Headers;

namespace Rewardergg.Infrastructure.Services
{
    public class StartggService : IStartggService
    {
        private readonly StartggSettings _startggSettings;

        public StartggService(IOptionsMonitor<StartggSettings> startggSettings)
        {
            _startggSettings = startggSettings.CurrentValue;
        }

        public async Task<EntrantStandingResult?> GetEntrantStandingAsync(string accessToken, string eventId, int userId, CancellationToken cancellationToken)
        {
            var graphQLClient = new GraphQLHttpClient(
                                _startggSettings.BaseUrl + _startggSettings.GraphQlEndpoint,
                                new NewtonsoftJsonSerializer());

            graphQLClient.HttpClient.DefaultRequestHeaders.Authorization =
                                new AuthenticationHeaderValue("Bearer", accessToken);
 

            var request = new GraphQLHttpRequest
            {
                Query = UserQueries.EntrantStanding,
                OperationName = "EntrantStanding",
                Variables = new
                {
                    eventId,
                    userId
                }
            };

            var response = await graphQLClient.SendQueryAsync<GraphQLResult<EntrantStandingResult>>(request, cancellationToken);

            if (response.AsGraphQLHttpResponse().StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error getting the entrant standing data from startgg service");
            }

            return response.Data.data ?? throw new Exception("Data from Player Account is null");
        }

        public async Task<CurrentUserResult> GetPlayerAccountData(string bearerToken, CancellationToken cancellationToken)
        {

            var graphQLClient = new GraphQLHttpClient(_startggSettings.BaseUrl + _startggSettings.GraphQlEndpoint, new NewtonsoftJsonSerializer());

            graphQLClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            var query = new GraphQLRequest()
            {
                Query = UserQueries.PlayerAccountData,
                OperationName = "PlayerData"
            };

            var request = new GraphQLHttpRequest(query);

            var response = await graphQLClient.SendQueryAsync<CurrentUserResult>(request, cancellationToken);

            if (response.AsGraphQLHttpResponse().StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error getting player account data from startgg service");
            }

            return response.Data ?? throw new Exception("Data from Player Account is null");
        }
    }
}

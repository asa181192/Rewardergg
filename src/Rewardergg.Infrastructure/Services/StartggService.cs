using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Rewardergg.Application.Configurations;
using Rewardergg.Application.GraphQlEntities;
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

        public async Task<Data> GetPlayerAccountData(string bearerToken)
        {

            var graphQLClient = new GraphQLHttpClient(_startggSettings.BaseUrl + _startggSettings.GraphQlEndpoint, new NewtonsoftJsonSerializer());

            graphQLClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            var query = new GraphQLRequest()
            {
                Query = UserQueries.PlayerAccountData,
                OperationName = "PlayerData"
            };

            var request = new GraphQLHttpRequest(query);
           
            var response = await graphQLClient.SendQueryAsync<Data>(request);

            if( response.AsGraphQLHttpResponse().StatusCode != System.Net.HttpStatusCode.OK ) 
            {
                throw new Exception("Error getting player account data from startgg service");
            }
            
            return response.Data ?? throw new Exception("Data from Player Account is null");
        }
    }
}

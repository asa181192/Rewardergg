using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.Configuration;
using Rewardergg.Application.GraphQlEntities;
using Rewardergg.Application.GraphQlQueries;
using Rewardergg.Application.Interfaces;
using System.Buffers.Text;
using System.Net.Http.Headers;

namespace Rewardergg.Infrastructure.Services
{
    public class StartggService : IStartggService
    {
        private readonly IConfiguration _configuration;
        private string _baseUrl;
        private string _graphQlEndpoint;

        public StartggService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["Startgg:BaseUrl"]!;
            _graphQlEndpoint = _configuration["Startgg:GraphQlEndpoint"]!;
        }

        public async Task<Data> GetPlayerAccountData(string bearerToken)
        {

            var graphQLClient = new GraphQLHttpClient(_baseUrl + _graphQlEndpoint, new NewtonsoftJsonSerializer());

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
            
            return response.Data;
        }
    }
}

using Rewardergg.Application.GraphQlEntities;

namespace Rewardergg.Application.Interfaces
{
    public interface IStartggService
    {
        Task<Data> GetPlayerAccountData(string bearerToken);
    }
}

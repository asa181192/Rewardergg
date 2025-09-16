using Rewardergg.Application.DTOs;
using Rewardergg.Application.GraphQlEntities.Results;

namespace Rewardergg.Application.Interfaces
{
    public interface IStartggService
    {
        Task<CurrentUserResult> GetPlayerAccountData(string bearerToken, CancellationToken cancellationToken);
        Task<EntrantStandingResult?> GetEntrantStandingAsync(string accessToken, string eventId, int userId, CancellationToken cancellationToken);
    }
}

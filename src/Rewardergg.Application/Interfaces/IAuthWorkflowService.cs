using Rewardergg.Application.DTOs;

namespace Rewardergg.Application.Interfaces
{
    public interface IAuthWorkflowService
    {
        Task<AuthResponse> LoginAsync(string code, CancellationToken cancellationToken);
        Task SyncUserDataAsync(Guid userId, CancellationToken cancellationToken);

        Task<AuthResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken); // Add this
    }
}

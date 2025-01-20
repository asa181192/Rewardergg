using Rewardergg.Domain;

namespace Rewardergg.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Tournament> Tournaments { get; }
        IGenericRepository<MatchResult> MatchResults { get; }

        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}

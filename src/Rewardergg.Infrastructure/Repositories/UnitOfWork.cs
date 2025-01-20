using Rewardergg.Application.Interfaces;
using Rewardergg.Domain;
using Rewardergg.Infrastructure.Persitence;

namespace Rewardergg.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IGenericRepository<User> _users;
        private IGenericRepository<Tournament> _tournaments;
        private IGenericRepository<MatchResult> _matchResults;

        public IGenericRepository<User> Users =>
            _users ??= new GenericRepository<User>(_context);

        public IGenericRepository<Tournament> Tournaments =>
            _tournaments ??= new GenericRepository<Tournament>(_context);

        public IGenericRepository<MatchResult> MatchResults =>
            _matchResults ??= new GenericRepository<MatchResult>(_context);

        public UnitOfWork(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

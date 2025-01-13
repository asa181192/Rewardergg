using Microsoft.EntityFrameworkCore;
using Rewardergg.Domain;
using Rewardergg.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Rewardergg.Infrastructure.Persitence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserToken { get; set; }
        public DbSet<RewardCatalog> RewardCatalog { get; set; }
        public DbSet<LeaderBoard> LeaderBoard { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventUser> EventParticipants { get; set; }
        public DbSet<MatchResult> MatchResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TournamentConfiguration());
            modelBuilder.ApplyConfiguration(new MatchResultConfiguration());
            modelBuilder.ApplyConfiguration(new LeaderBoardConfiguration());
            modelBuilder.ApplyConfiguration(new EventUserConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());

        }
    }
}

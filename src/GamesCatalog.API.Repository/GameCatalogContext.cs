using GamesCatalog.API.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GamesCatalog.API.Repository
{
    public class GameCatalogContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<UserFavoriteGames> FavoriteGames { get; set; }

        public GameCatalogContext(DbContextOptions<GameCatalogContext> options)
            : base(options)
        {
        }

        public override int SaveChanges()
        {
            SetUpdatedDate();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetUpdatedDate();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetUpdatedDate();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetUpdatedDate();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFavoriteGames>().HasKey(bc => new { bc.UserId, bc.GameId });
            modelBuilder.Entity<UserFavoriteGames>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.FavoriteGames)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<UserFavoriteGames>()
                .HasOne(bc => bc.Game)
                .WithMany(c => c.FavoriteGames)
                .HasForeignKey(bc => bc.GameId);
        }

        private void SetUpdatedDate()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Modified || e.State == EntityState.Added));

            foreach (var entityEntry in entries)
            {
                (entityEntry.Entity as BaseEntity).Updated = DateTimeOffset.UtcNow;
            }

        }
    }
}

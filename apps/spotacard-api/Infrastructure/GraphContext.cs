using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Spotacard.Domain;
using System.Data;

namespace Spotacard.Infrastructure
{
  public class GraphContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;

        public GraphContext(DbContextOptions options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CardTag> CardTags { get; set; }
        public DbSet<CardFavorite> CardFavorites { get; set; }
        public DbSet<FollowedPeople> FollowedPeople { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardTag>(b =>
            {
                b.HasKey(t => new { t.CardId, t.TagId });

                b.HasOne(pt => pt.Card)
                .WithMany(p => p.CardTags)
                .HasForeignKey(pt => pt.CardId);

                b.HasOne(pt => pt.Tag)
                .WithMany(t => t.CardTags)
                .HasForeignKey(pt => pt.TagId);
            });

            modelBuilder.Entity<CardFavorite>(b =>
            {
                b.HasKey(t => new { t.CardId, t.PersonId });

                b.HasOne(pt => pt.Card)
                    .WithMany(p => p.CardFavorites)
                    .HasForeignKey(pt => pt.CardId);

                b.HasOne(pt => pt.Person)
                    .WithMany(t => t.CardFavorites)
                    .HasForeignKey(pt => pt.PersonId);
            });

            modelBuilder.Entity<FollowedPeople>(b =>
            {
                b.HasKey(t => new { t.ObserverId, t.TargetId });

                // we need to add OnDelete RESTRICT otherwise for the SqlServer database provider, 
                // app.ApplicationServices.GetRequiredService<GraphContext>().Database.EnsureCreated(); throws the following error:
                // System.Data.SqlClient.SqlException
                // HResult = 0x80131904
                // Message = Introducing FOREIGN KEY constraint 'FK_FollowedPeople_Persons_TargetId' on table 'FollowedPeople' may cause cycles or multiple cascade paths.Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
                // Could not create constraint or index. See previous errors.
                b.HasOne(pt => pt.Observer)
                    .WithMany(p => p.Followers)
                    .HasForeignKey(pt => pt.ObserverId)
                    .OnDelete(DeleteBehavior.Restrict);

                // we need to add OnDelete RESTRICT otherwise for the SqlServer database provider, 
                // app.ApplicationServices.GetRequiredService<GraphContext>().Database.EnsureCreated(); throws the following error:
                // System.Data.SqlClient.SqlException
                // HResult = 0x80131904
                // Message = Introducing FOREIGN KEY constraint 'FK_FollowingPeople_Persons_TargetId' on table 'FollowedPeople' may cause cycles or multiple cascade paths.Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
                // Could not create constraint or index. See previous errors.
                b.HasOne(pt => pt.Target)
                    .WithMany(t => t.Following)
                    .HasForeignKey(pt => pt.TargetId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        #region Transaction Handling
        public void BeginTransaction()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            if (!Database.IsInMemory())
            {
                _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
            }
        }

        public void CommitTransaction()
        {
            try
            {
                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        #endregion
    }
}

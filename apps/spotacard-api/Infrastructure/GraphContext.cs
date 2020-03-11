using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Spotacard.Domain;

namespace Spotacard.Infrastructure
{
    public class GraphContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;

        public GraphContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Card> Cards { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CardAttribute> Attributes { get; set; }
        public DbSet<CardTag> CardTags { get; set; }
        public DbSet<CardFavorite> CardFavorites { get; set; }
        public DbSet<FollowedPeople> FollowedPeople { get; set; }
        public DbSet<Edge> Edges { get; set; }
        public DbSet<App> Apps { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<Layout> Layouts { get; set; }
        public DbSet<Widget> Widgets { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Table> Tables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cell>(b =>
            {
                b.HasKey(t => t.Id);

                b.HasOne(pt => pt.Page)
                    .WithMany(p => p.Cells)
                    .HasForeignKey(pt => pt.PageId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(pt => pt.Widget)
                    .WithMany(p => p.Cells)
                    .HasForeignKey(pt => pt.WidgetId)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(pt => pt.Field)
                    .WithMany(p => p.Cells)
                    .HasForeignKey(pt => pt.FieldId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Page>(b =>
            {
                b.HasKey(t => t.Id);

                b.HasOne(pt => pt.Table)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(pt => pt.TableId)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(pt => pt.App)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(pt => pt.AppId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(pt => pt.Layout)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(pt => pt.LayoutId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Field>(b =>
            {
                b.HasKey(t => t.Id);

                b.HasOne(pt => pt.Table)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(pt => pt.TableId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(pt => pt.Widget)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(pt => pt.WidgetId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Table>(b =>
            {
                b.HasKey(t => t.Id);

                b.HasOne(pt => pt.App)
                    .WithMany(p => p.Tables)
                    .HasForeignKey(pt => pt.AppId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Edge>(b =>
            {
                b.HasKey(t => new { t.ParentId, t.ChildId });
                b.HasOne(pt => pt.Parent)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(pt => pt.ParentId)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(pt => pt.Child)
                    .WithMany(t => t.Children)
                    .HasForeignKey(pt => pt.ChildId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Card>(card => {
                card.HasIndex(e => e.Slug).IsUnique();
            });

            modelBuilder.Entity<CardTag>(b =>
            {
                b.HasKey(t => new {t.CardId, t.TagId});

                b.HasOne(pt => pt.Card)
                    .WithMany(p => p.CardTags)
                    .HasForeignKey(pt => pt.CardId);

                b.HasOne(pt => pt.Tag)
                    .WithMany(t => t.CardTags)
                    .HasForeignKey(pt => pt.TagId);
            });

            modelBuilder.Entity<CardFavorite>(b =>
            {
                b.HasKey(t => new {t.CardId, t.PersonId});

                b.HasOne(pt => pt.Card)
                    .WithMany(p => p.CardFavorites)
                    .HasForeignKey(pt => pt.CardId);

                b.HasOne(pt => pt.Person)
                    .WithMany(t => t.CardFavorites)
                    .HasForeignKey(pt => pt.PersonId);
            });

            modelBuilder.Entity<FollowedPeople>(b =>
            {
                b.HasKey(t => new {t.ObserverId, t.TargetId});

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

            modelBuilder.Entity<CardAttribute>(b =>
            {
                b.HasKey(t => t.Id);

                b.HasOne(pt => pt.Card)
                    .WithMany(p => p.CardAttributes)
                    .HasForeignKey(pt => pt.CardId);
            });
        }

        #region Transaction Handling

        public void BeginTransaction()
        {
            if (_currentTransaction != null) return;

            if (!Database.IsInMemory()) _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
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

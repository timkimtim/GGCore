using GGCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GGCore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Other configuration left out to focus on many-to-many
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Title = "Post 1",
                    Content = "Content of First Post"
                },
                new Post
                {
                    Id = 2,
                    Title = "Post 2",
                    Content = "Content of Second Post"
                },
                new Post
                {
                    Id = 3,
                    Title = "Post 3",
                    Content = "Content of Third Post"
                });
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = 1,
                    Text = "Comment 1",
                    PostId = 1
                },
                new Comment
                {
                    Id = 2,
                    Text = "Comment 2",
                    PostId = 2
                },
                new Comment
                {
                    Id = 3,
                    Text = "Comment 3",
                    PostId = 3
                });
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Entity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified)); 

            foreach (var entityEntry in entries)
            {
                ((Entity)entityEntry.Entity).Updated = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((Entity)entityEntry.Entity).Created = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}

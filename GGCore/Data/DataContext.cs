using GGCore.Configs;
using GGCore.Configs.Seeders;
using GGCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace GGCore.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleSeeder());
            builder.ApplyConfiguration(new PostSeeder());
            builder.ApplyConfiguration(new CommentSeeder());

            // Change default Identity table names
            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");  
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");

            });
        }

        //public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    var entries = ChangeTracker
        //        .Entries()
        //        .Where(e => e.Entity is Entity && (
        //                e.State == EntityState.Added
        //                || e.State == EntityState.Modified)); 

        //    foreach (var entityEntry in entries)
        //    {
        //        ((Entity)entityEntry.Entity).Updated = DateTime.Now;

        //        if (entityEntry.State == EntityState.Added)
        //        {
        //            ((Entity)entityEntry.Entity).Created = DateTime.Now;
        //        }
        //    }

        //    return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //}
    }
}

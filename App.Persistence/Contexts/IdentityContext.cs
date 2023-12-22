using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, Role, string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        //#region Identity       
        //public DbSet<Permission> Permissions { get; set; }
        //public DbSet<ApplicationPermission> ApplicationPermissions { get; set; }
        //public DbSet<ApplicationRole> ApplicationRole { get; set; }
        //public DbSet<UserApplicationRole> UserApplicationRoles { get; set; }
        //#endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.HasDefaultSchema("Identity");
           
            //modelBuilder.Entity<ApplicationUser>(entity =>
            //{
            //    entity.ToTable(name: "User");
            //});

            //modelBuilder.Entity<IdentityRole<string>>(entity =>
            //{
            //    entity.ToTable(name: "Role");
            //});
            //modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            //{
            //    entity.ToTable("UserRoles");
            //});

            //modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            //{
            //    entity.ToTable("UserClaims");
            //});

            //modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            //{
            //    entity.ToTable("UserLogins");
            //});

            //modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            //{
            //    entity.ToTable("RoleClaims");
            //});

            //modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            //{
            //    entity.ToTable("UserTokens");
            //});

            //modelBuilder.Entity<ApplicationPermission>(entity =>
            //{
            //    entity.ToTable("ApplicationPermissions");
            //});

            //modelBuilder.Entity<UserApplicationRole>(entity =>
            //{
            //    entity.ToTable("UserApplicationRole");
            //});

            //modelBuilder.Entity<ApplicationRole>(entity =>
            //{
            //    entity.ToTable("ApplicationRole");
            //});


            // modelBuilder.Seed();
        }
    }
}

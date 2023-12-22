using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Identity;
using Shared.Constants;
using Persistence.Mapping.Application;
using Microsoft.EntityFrameworkCore.Design;
using Domain.Entities.Application;

namespace Persistence.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, string>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer("Server=DESKTOP-TOOMNMP;Database=Employee-DataBase;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }

        #region Application     
      
        public DbSet<Employee> Employees { get; set; }

        #endregion


        #region Identity
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<ApplicationPermission> ApplicationPermissions { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<UserApplicationRole> UserApplicationRoles { get; set; }
        public DbSet<ApplicationRolePermission> ApplicationRolePermissions { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Application
           
            modelBuilder.AddEmployeeMapping();
            #endregion

            #region Identity
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User", EntitySchema.IdentitySchema);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "Role", EntitySchema.IdentitySchema);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable(name: "Permission", EntitySchema.IdentitySchema);
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", EntitySchema.IdentitySchema);
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", EntitySchema.IdentitySchema);
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", EntitySchema.IdentitySchema);
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims", EntitySchema.IdentitySchema);
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", EntitySchema.IdentitySchema);
            });

            modelBuilder.Entity<ApplicationPermission>(entity =>
            {
                entity.ToTable("ApplicationPermission", EntitySchema.IdentitySchema);
            });

            modelBuilder.Entity<ApplicationRolePermission>(entity =>
            {
                entity.ToTable("ApplicationRolePermission", EntitySchema.IdentitySchema);
            });

            modelBuilder.Entity<UserApplicationRole>(entity =>
            {
                entity.ToTable("UserApplicationRole", EntitySchema.IdentitySchema);
            });

            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable("ApplicationRole", EntitySchema.IdentitySchema);
            });
            #endregion

            base.OnModelCreating(modelBuilder);
            //modelBuilder.Seed();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}

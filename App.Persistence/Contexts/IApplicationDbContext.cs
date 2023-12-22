using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Domain.Entities.Identity;
using Domain.Entities.Application;

namespace Persistence.Contexts
{
    public interface IApplicationDbContext
    {

        #region Application       
        DbSet<Employee> Employees { get; set; }
        #endregion


        #region Identity
        DbSet<Role> Roles { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<ApplicationPermission> ApplicationPermissions { get; set; }
        DbSet<ApplicationRole> ApplicationRole { get; set; }
        DbSet<UserApplicationRole> UserApplicationRoles { get; set; }
        DbSet<ApplicationRolePermission> ApplicationRolePermissions { get; set; }
        #endregion
        Task<int> SaveChangesAsync();
    }
}

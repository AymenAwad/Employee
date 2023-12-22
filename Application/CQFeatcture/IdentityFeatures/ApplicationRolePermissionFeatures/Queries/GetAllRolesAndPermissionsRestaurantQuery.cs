using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Persistence.Contexts;
using Shared.Wrappers;

namespace Application.Features.IdentityFeatures.ApplicationRolePermissionFeatures.Queries
{
    public class GetAllRolesAndPermissionsAcademiesQuery : IRequest<Response<List<RolesAndPermissionsRestaurantyDto>>>
    {
        public int AcademyId { get; set; }
        public class GetAllRolesAndPermissionsAcademiesQueryHander : IRequestHandler<GetAllRolesAndPermissionsAcademiesQuery, Response<List<RolesAndPermissionsRestaurantyDto>>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllRolesAndPermissionsAcademiesQueryHander(IApplicationDbContext context)
            {
                _context = context;
            }

            public  async Task<Response<List<RolesAndPermissionsRestaurantyDto>>> Handle(GetAllRolesAndPermissionsAcademiesQuery request, CancellationToken cancellationToken)
            {
                Response<List<RolesAndPermissionsRestaurantyDto>> result = new Response<List<RolesAndPermissionsRestaurantyDto>>();
                List<RolesAndPermissionsRestaurantyDto> rpo = new List<RolesAndPermissionsRestaurantyDto>();

                try
                {
                    List<RolesAndPermissionsRestaurantyDto> rolesPermissions = (from applicationRole in _context.ApplicationRole
                                                                                 join applicationRolePermission in _context.ApplicationRolePermissions
                                                                                 on applicationRole.Id equals applicationRolePermission.ApplicationRoleId

                                                                                 join applicationPermission in _context.ApplicationPermissions
                                                                                 on applicationRolePermission.ApplicationPermissionsId equals applicationPermission.Id

                                                                                 join role in _context.Roles
                                                                                 on applicationRole.RoleId equals role.Id

                                                                                 join permission in _context.Permissions
                                                                                 on applicationPermission.PermissionId equals permission.Id

                                                                                 join Restaurant in _context.Employees
                                                                                 on applicationRole.EmployeeId equals Restaurant.Id
                                                                                 select new RolesAndPermissionsRestaurantyDto
                                                                                 {
                                                                                     ApplicationRolePermissionId = applicationRolePermission.Id,
                                                                                     RoleId = applicationRole.Id,
                                                                                     RoleNameAr = role.NameAr,
                                                                                     RoleNameEn = role.Name,
                                                                                     PermissionId = applicationPermission.Id,
                                                                                     PermissionNameAr = permission.NameAr,
                                                                                     PermissionNameEn = permission.NameEn,
                                                                                     PermissionKey = permission.Key,
                                                                                     RestaurantId = applicationRole.EmployeeId.Value,
                                                                                     RestPermissionId = applicationPermission.EmployeeId.Value,
                                                                                     RestaurantName = Restaurant.Name,
                                                                                     RoleStatus = role.Status,
                                                                                     RestaurantStatus = Restaurant.Status
                                                                                 }).ToList();

                    if (request.AcademyId > 0)
                    {
                        rolesPermissions = rolesPermissions.AsQueryable().Where(x => x.RestaurantId == request.AcademyId).ToList();
                    }
                    result.Data = new List<RolesAndPermissionsRestaurantyDto>();
                    result.Data = rolesPermissions;

                    if (result.Data.Count > 0)
                        return new Response<List<RolesAndPermissionsRestaurantyDto>>(HttpStatusCode.OK, result.Data, "Successfully!", null);

                    return new Response<List<RolesAndPermissionsRestaurantyDto>>(HttpStatusCode.NotFound, result.Data, "Not found data!", null);

                }
                catch (System.Exception ex)
                {

                    return new Response<List<RolesAndPermissionsRestaurantyDto>>(HttpStatusCode.BadRequest, result.Data, $"bad request {ex.Message} data!", null);

                }
            }
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Domain.Entities.Identity;
using Persistence.Contexts;
using Shared.Wrappers;

namespace Application.Features.IdentityFeatures.ApplicationRolePermissionFeatures.Queries
{
    public class GetAllRolesAndPermissionsOrganizationsQuery : IRequest<Response<List<RolesAndPermissionsOrganizationDto>>>
    {
        public int OrganizationId { get; set; }
        public class GetAllRolesAndPermissionsOrganizationsQueryHandler : IRequestHandler<GetAllRolesAndPermissionsOrganizationsQuery, Response<List<RolesAndPermissionsOrganizationDto>>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllRolesAndPermissionsOrganizationsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response<List<RolesAndPermissionsOrganizationDto>>> Handle(GetAllRolesAndPermissionsOrganizationsQuery request, CancellationToken cancellationToken)
            {
                Response<List<RolesAndPermissionsOrganizationDto>> result = new Response<List<RolesAndPermissionsOrganizationDto>>();
                List<RolesAndPermissionsOrganizationDto> rpo = new List<RolesAndPermissionsOrganizationDto>();

                try
                {
                    List<RolesAndPermissionsOrganizationDto> rolesPermissions =  (from applicationRole in _context.ApplicationRole
                                                  join applicationRolePermission in _context.ApplicationRolePermissions
                                                  on applicationRole.Id equals applicationRolePermission.ApplicationRoleId

                                                  join applicationPermission in _context.ApplicationPermissions
                                                  on applicationRolePermission.ApplicationPermissionsId equals applicationPermission.Id

                                                  join role in _context.Roles
                                                  on applicationRole.RoleId equals role.Id

                                                  join permission in _context.Permissions
                                                  on applicationPermission.PermissionId equals permission.Id

                                                  join organization in _context.Employees
                                                  on applicationRole.EmployeeId equals organization.Id
                                                  select new RolesAndPermissionsOrganizationDto
                                                  {
                                                      ApplicationRolePermissionId = applicationRolePermission.Id,
                                                      RoleId = applicationRole.Id,
                                                      RoleNameAr = role.NameAr,
                                                      RoleNameEn = role.Name,
                                                      PermissionId = applicationPermission.Id,
                                                      PermissionNameAr = permission.NameAr,
                                                      PermissionNameEn = permission.NameEn,
                                                      PermissionKey = permission.Key,
                                                      OrganizationId = applicationRole.EmployeeId.Value,
                                                      OrgPermissionId = applicationPermission.EmployeeId.Value,
                                                      OrganizationNameEn = organization.Name,
                                                      RoleStatus = role.Status,
                                                      OrganizationStatus = organization.Status
                                                  }).ToList();

                    if (request.OrganizationId > 0)
                    {
                        rolesPermissions = rolesPermissions.AsQueryable().Where(x => x.OrganizationId == request.OrganizationId).ToList();
                    }
                    result.Data = new List<RolesAndPermissionsOrganizationDto>();
                    result.Data = rolesPermissions;

                    if (result.Data.Count > 0)
                        return new Response<List<RolesAndPermissionsOrganizationDto>>(HttpStatusCode.OK, result.Data, "Successfully!", null);

                    return new Response<List<RolesAndPermissionsOrganizationDto>>(HttpStatusCode.NotFound, result.Data, "Not found data!", null);

                }
                catch (System.Exception ex)
                {

                    return new Response<List<RolesAndPermissionsOrganizationDto>>(HttpStatusCode.BadRequest, result.Data, $"bad request {ex.Message} data!", null);

                }

            }
        }
    }
}

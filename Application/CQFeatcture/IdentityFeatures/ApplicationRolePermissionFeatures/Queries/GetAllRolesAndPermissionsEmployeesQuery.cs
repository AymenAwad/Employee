using MediatR;
using System;
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
    public class GetAllRolesAndPermissionsEmployeesQuery : IRequest<Response<List<RolesAndPermissionsApplicantDto>>>
    {
        public int ApplicantId { get; set; }
        public class GetAllRolesAndPermissionsApplicantsQueryHander : IRequestHandler<GetAllRolesAndPermissionsEmployeesQuery, Response<List<RolesAndPermissionsApplicantDto>>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllRolesAndPermissionsApplicantsQueryHander(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response<List<RolesAndPermissionsApplicantDto>>> Handle(GetAllRolesAndPermissionsEmployeesQuery request, CancellationToken cancellationToken)
            {
                Response<List<RolesAndPermissionsApplicantDto>> result = new Response<List<RolesAndPermissionsApplicantDto>>();
                List<RolesAndPermissionsApplicantDto> rpo = new List<RolesAndPermissionsApplicantDto>();

                try
                {
                    List<RolesAndPermissionsApplicantDto> rolesPermissions = (from applicationRole in _context.ApplicationRole
                                                                            join applicationRolePermission in _context.ApplicationRolePermissions
                                                                            on applicationRole.Id equals applicationRolePermission.ApplicationRoleId

                                                                            join applicationPermission in _context.ApplicationPermissions
                                                                            on applicationRolePermission.ApplicationPermissionsId equals applicationPermission.Id

                                                                            join role in _context.Roles
                                                                            on applicationRole.RoleId equals role.Id

                                                                            join permission in _context.Permissions
                                                                            on applicationPermission.PermissionId equals permission.Id

                                                                            join applicant in _context.Employees
                                                                            on applicationRole.EmployeeId equals applicant.Id
                                                                            select new RolesAndPermissionsApplicantDto
                                                                            {
                                                                                ApplicationRolePermissionId = applicationRolePermission.Id,
                                                                                RoleId = applicationRole.Id,
                                                                                RoleNameAr = role.NameAr,
                                                                                RoleNameEn = role.Name,
                                                                                PermissionId = applicationPermission.Id,
                                                                                PermissionNameAr = permission.NameAr,
                                                                                PermissionNameEn = permission.NameEn,
                                                                                PermissionKey = permission.Key,
                                                                                ApplicantId = applicationRole.EmployeeId.Value,
                                                                                ApntPermissionId = applicationPermission.EmployeeId.Value,
                                                                                ApplicantNameEn = applicant.Name,
                                                                                RoleStatus = role.Status,
                                                                                ApplicantStatus = applicant.Status
                                                                            }).ToList();

                    if (request.ApplicantId > 0)
                    {
                        rolesPermissions = rolesPermissions.AsQueryable().Where(x => x.ApplicantId == request.ApplicantId).ToList();
                    }
                    result.Data = new List<RolesAndPermissionsApplicantDto>();
                    result.Data = rolesPermissions;

                    if (result.Data.Count > 0)
                        return new Response<List<RolesAndPermissionsApplicantDto>>(HttpStatusCode.OK, result.Data, "Successfully!", null);

                    return new Response<List<RolesAndPermissionsApplicantDto>>(HttpStatusCode.NotFound, result.Data, "Not found data!", null);

                }
                catch (System.Exception ex)
                {

                    return new Response<List<RolesAndPermissionsApplicantDto>>(HttpStatusCode.BadRequest, result.Data, $"bad request {ex.Message} data!", null);

                }
            }
        }
    }
}

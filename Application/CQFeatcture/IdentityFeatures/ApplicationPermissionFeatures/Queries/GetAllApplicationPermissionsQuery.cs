using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Domain.Enum;
using Persistence.Contexts;
using Shared.Globalization;
using Shared.Wrappers;

namespace Application.Features.IdentityFeatures.ApplicationPermissionFeatures.Queries
{
    public class GetAllApplicationPermissionsQuery : IRequest<Response<List<ApplicationPermissionDto>>>
    {
        public int ApplicationId { get; set; } //AcademyId, OrganizationId, OR ApplicantId
        public int ApplicationTypeId { get; set; } //enum AcademyId = 2, OrganizationId = 1, or ApplicantId = 3
        public class GetAllApplicationPermissionsQueryHandler : IRequestHandler<GetAllApplicationPermissionsQuery, Response<List<ApplicationPermissionDto>>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllApplicationPermissionsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response<List<ApplicationPermissionDto>>> Handle(GetAllApplicationPermissionsQuery request, CancellationToken cancellationToken)
            {
                Response<List<ApplicationPermissionDto>> result = new Response<List<ApplicationPermissionDto>>();
                try
                {
                    List<ApplicationPermissionDto> applicationPermissions = new List<ApplicationPermissionDto>();
                    //=================filters ApplicationId == 0 ===============================
                    if (request.ApplicationTypeId == (int)ApplicationTypeEnum.Employee && request.ApplicationId == 0)
                    {
                        applicationPermissions = await _context.ApplicationPermissions.Where(a => a.EmployeeId != null && a.EmployeeId > 0).Select(p => new ApplicationPermissionDto()
                        {
                            Id = p.Id.ToString(),
                            ApplicationType = ((int)ApplicationTypeEnum.Employee).ToString(),
                            PermissionId = p.PermissionId.ToString(),
                            PermissionKey = p.Permission.Key,
                            ApplicationId = p.EmployeeId.ToString(),
                            ApplicationName = p.Employee.Name
                        }).ToListAsync();
                    }

                    result.Data = applicationPermissions;

                    if (applicationPermissions.Any())
                    {

                        return new Response<List<ApplicationPermissionDto>>(HttpStatusCode.OK, result.Data, "Successfully!", null);
                    }

                    return new Response<List<ApplicationPermissionDto>>(HttpStatusCode.NotFound, result.Data, "Not found data!", null);
                }
                catch (Exception ex)
                {
                    return new Response<List<ApplicationPermissionDto>>(HttpStatusCode.BadRequest, result.Data, $"bad request! {ex.Message}", null);

                }
            }
        }
    }
}
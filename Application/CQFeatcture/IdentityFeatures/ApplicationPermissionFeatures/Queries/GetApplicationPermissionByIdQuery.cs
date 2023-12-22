using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Domain.Enum;
using Persistence.Contexts;
using Shared.Globalization;
using Shared.Wrappers;

namespace Application.Features.IdentityFeatures.ApplicationPermissionFeatures.Queries
{
    public class GetApplicationPermissionByIdQuery : IRequest<Response<ApplicationPermissionDto>>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ApplicationId { get; set; } //AcademyId, OrganizationId, OR ApplicantId
        [Required]
        public int ApplicationTypeId { get; set; } //enum AcademyId = 2, OrganizationId = 1, or ApplicantId = 3
        public class GetApplicationPermissionByIdQueryHandler : IRequestHandler<GetApplicationPermissionByIdQuery, Response<ApplicationPermissionDto>>
        {
            private readonly IApplicationDbContext _context;
            public GetApplicationPermissionByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response<ApplicationPermissionDto>> Handle(GetApplicationPermissionByIdQuery request, CancellationToken cancellationToken)
            {
                Response<ApplicationPermissionDto> result = new Response<ApplicationPermissionDto>();
                try
                {
                    ApplicationPermissionDto applicationPermission = new ApplicationPermissionDto();

                    if (request.ApplicationTypeId == (int)ApplicationTypeEnum.Employee)
                    {
                        applicationPermission = await _context.ApplicationPermissions.Where(a => a.Id == request.Id && a.EmployeeId == request.ApplicationId).Select(p => new ApplicationPermissionDto()
                        {
                            Id = p.Id.ToString(),
                            ApplicationType = ((int)ApplicationTypeEnum.Employee).ToString(),
                            PermissionId = p.PermissionId.ToString(),
                            PermissionKey = p.Permission.Key,
                            ApplicationId = p.EmployeeId.ToString(),
                            ApplicationName = p.Employee.Name
                        }).FirstOrDefaultAsync();
                    }

                    result.Data = applicationPermission;

                    if (applicationPermission != null)
                    {

                        return new Response<ApplicationPermissionDto>(HttpStatusCode.OK, result.Data, "Successfully!", null);

                    }

                    return new Response<ApplicationPermissionDto>(HttpStatusCode.NotFound, result.Data, "Not found data!", null);

                }
                catch (Exception ex)
                {
                    return new Response<ApplicationPermissionDto>(HttpStatusCode.BadRequest, result.Data, $"bad request! {ex.Message}", null);

                }
            }
        }
    }
}
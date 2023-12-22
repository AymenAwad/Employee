using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
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

namespace Application.Features.IdentityFeatures.ApplicationRoleFeatures.Queries
{
    public class GetApplicationRoleByIdQuery : IRequest<Response<ApplicationRoleDto>>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ApplicationId { get; set; } //EmployeeId
        [Required]
        public int ApplicationTypeId { get; set; } //enum EmployeeId = 1
        public class GetApplicationRoleByIdQueryHandler : IRequestHandler<GetApplicationRoleByIdQuery, Response<ApplicationRoleDto>>
        {
            private readonly IApplicationDbContext _context;
            public GetApplicationRoleByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response<ApplicationRoleDto>> Handle(GetApplicationRoleByIdQuery request, CancellationToken cancellationToken)
            {
                Response<ApplicationRoleDto> result = new Response<ApplicationRoleDto>();
                try
                {
                    ApplicationRoleDto applicationPermission = new ApplicationRoleDto();

                    if (request.ApplicationTypeId == (int)ApplicationTypeEnum.Employee)
                    {
                        applicationPermission = await _context.ApplicationRole.Where(a => a.Id == request.Id && a.EmployeeId == request.ApplicationId).Select(p => new ApplicationRoleDto()
                        {
                            Id = p.Id,
                            ApplicationType = ((int)ApplicationTypeEnum.Employee).ToString(),
                            RoleId = p.RoleId,
                            RoleName = CultureHelper.IsArabic ? p.Role.NameAr : p.Role.Name,
                            ApplicationId = p.EmployeeId.ToString(),
                            ApplicationName = p.Employee.Name
                        }).FirstOrDefaultAsync();
                    }

                    result.Data = applicationPermission;

                    if (applicationPermission != null)
                    {

                        return new Response<ApplicationRoleDto>(HttpStatusCode.OK, result.Data, "Successfully!", null);

                    }

                    return new Response<ApplicationRoleDto>(HttpStatusCode.NotFound, result.Data, "Not found data!", null);

                }
                catch (Exception ex)
                {
                    return new Response<ApplicationRoleDto>(HttpStatusCode.BadRequest, result.Data, $"bad request! {ex.Message}", null);

                }
            }
        }
    }
}
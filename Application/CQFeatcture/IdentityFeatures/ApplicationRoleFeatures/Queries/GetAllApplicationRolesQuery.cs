using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Domain.Enum;
using Persistence.Contexts;
using Shared.Globalization;
using Shared.Wrappers;

namespace Application.Features.IdentityFeatures.ApplicationRoleFeatures.Queries
{
    public class GetAllApplicationRolesQuery : IRequest<Response<List<ApplicationRoleDto>>>
    {
        public int ApplicationId { get; set; } //EmpoloyeeId,
        public int ApplicationTypeId { get; set; } //, EmpoloyeeId = 1
        public class GetAllApplicationRolesQueryHandler : IRequestHandler<GetAllApplicationRolesQuery, Response<List<ApplicationRoleDto>>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllApplicationRolesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response<List<ApplicationRoleDto>>> Handle(GetAllApplicationRolesQuery request, CancellationToken cancellationToken)
            {
                Response<List<ApplicationRoleDto>> result = new Response<List<ApplicationRoleDto>>();
                try
                {
                    List<ApplicationRoleDto> applicationRoles = new List<ApplicationRoleDto>();
                    //=================filters ApplicationId == 0 ===============================
                    if (request.ApplicationTypeId == (int)ApplicationTypeEnum.Employee && request.ApplicationId == 0)
                    {
                        applicationRoles = await _context.ApplicationRole.Where(a => a.EmployeeId != null && a.EmployeeId > 0).Select(p => new ApplicationRoleDto()
                        {
                            Id = p.Id,
                            ApplicationType = ((int)ApplicationTypeEnum.Employee).ToString(),
                            RoleId = p.RoleId,
                            RoleName = CultureHelper.IsArabic ? p.Role.NameAr : p.Role.Name,
                            ApplicationId = p.EmployeeId.ToString(),
                            ApplicationName =  p.Employee.Name
                        }).ToListAsync();
                    }

                    result.Data = applicationRoles;

                    if (applicationRoles.Any())
                    {

                        return new Response<List<ApplicationRoleDto>>(HttpStatusCode.OK, result.Data, "Successfully!", null);

                    }

                    return new Response<List<ApplicationRoleDto>>(HttpStatusCode.NotFound, result.Data, "Not found data!", null);

                }
                catch (Exception ex)
                {
                    return new Response<List<ApplicationRoleDto>>(HttpStatusCode.BadRequest, result.Data, $"bad request! {ex.Message}", null);

                }
            }
        }
    }
}
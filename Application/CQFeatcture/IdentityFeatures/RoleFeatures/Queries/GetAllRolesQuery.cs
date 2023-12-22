using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Domain.Enum;
using Persistence.Contexts;
using Shared.Globalization;
using Shared.Wrappers;

namespace Application.Features.IdentityFeatures.RoleFeatures.Queries
{
    public class GetAllRolesQuery : IRequest<Response<List<RoleDto>>>
    {
        public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Response<List<RoleDto>>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllRolesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response<List<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
            {
                Response<List<RoleDto>> result = new Response<List<RoleDto>>();
                try
                {

                    List<RoleDto> roles = await _context.Roles.Where(x => x.Status == (int)StatusEnum.Active).Select(p => new RoleDto()
                    {
                        Id = p.Id.ToString(),
                        Name = CultureHelper.IsArabic ? p.NameAr : p.Name,
                        CreateBy = p.CreateBy,
                        CreationDate = p.CreationDate.ToShortDateString(),
                        ModifiedBy = p.ModifiedBy,
                        LastUpdateDate = p.LastUpdateDate.Value.ToShortDateString(),
                        Status = p.Status
                    }).ToListAsync();


                    result.Data = roles;

                    if (roles.Any())
                    {

                        return new Response<List<RoleDto>>(HttpStatusCode.OK, result.Data, "Successfully!", null);

                    }

                    return new Response<List<RoleDto>>(HttpStatusCode.NotFound, result.Data, "Not found data!", null);

                }
                catch (Exception ex)
                {
                    return new Response<List<RoleDto>>(HttpStatusCode.BadRequest, result.Data, $"bad request! {ex.Message}", null);

                }
            }
        }
    }
}
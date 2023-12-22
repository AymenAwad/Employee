using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
//using Application.Dtos.Lookup;
//using Application.Features.LookupsFeatures.CityFeatures.Queries;
using Domain.Enum;
using Persistence.Contexts;
using Shared.Globalization;
using Shared.Wrappers;

namespace Application.Features.IdentityFeatures.PermissionFeatures.Queries
{
    public class GetAllPermissionsQuery : IRequest<Response<List<PermissionDto>>>
    {
        public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, Response<List<PermissionDto>>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllPermissionsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response<List<PermissionDto>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
            {
                Response<List<PermissionDto>> result = new Response<List<PermissionDto>>();
                try
                {

                    List<PermissionDto> permissions = await _context.Permissions.Where(x => x.Status == (int)StatusEnum.Active).Select(p => new PermissionDto()
                    {
                        Id = p.Id.ToString(),
                        Key = p.Key,
                        Name = CultureHelper.IsArabic ? p.NameAr : p.NameEn,
                        CreateBy = p.CreateBy,
                        CreationDate = p.CreationDate.ToShortDateString(),
                        ModifiedBy = p.ModifiedBy,
                        LastUpdateDate = p.LastUpdateDate.Value.ToShortDateString(),
                        Status = p.Status.ToString()
                    }).ToListAsync();
                    

                    result.Data = permissions;

                    if (permissions.Any())
                    {

                        return new Response<List<PermissionDto>>(HttpStatusCode.OK, result.Data, "Successfully!", null);

                    }

                    return new Response<List<PermissionDto>>(HttpStatusCode.NotFound, result.Data, "Not found data!", null);

                }
                catch (Exception ex)
                {
                    return new Response<List<PermissionDto>>(HttpStatusCode.BadRequest, result.Data, $"bad request! {ex.Message}", null);

                }
            }
        }
    }
}
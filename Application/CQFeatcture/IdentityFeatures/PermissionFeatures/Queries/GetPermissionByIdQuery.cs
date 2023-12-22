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

namespace Application.Features.IdentityFeatures.PermissionFeatures.Queries
{
    public class GetPermissionByIdQuery : IRequest<Response<PermissionDto>>
    {
        public int Id { get; set; }
        public class GetPermissionByIdQueryHandler : IRequestHandler<GetPermissionByIdQuery, Response<PermissionDto>>
        {
            private readonly IApplicationDbContext _context;
            public GetPermissionByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response<PermissionDto>> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
            {
                Response<PermissionDto> result = new Response<PermissionDto>();
                try
                {

                    PermissionDto permission = await _context.Permissions.Where(x => x.Status == (int)StatusEnum.Active && x.Id == request.Id).Select(p => new PermissionDto()
                    {
                        Id = p.Id.ToString(),
                        Key = p.Key,
                        Name = CultureHelper.IsArabic ? p.NameAr : p.NameEn,
                        CreateBy = p.CreateBy,
                        CreationDate = p.CreationDate.ToShortDateString(),
                        ModifiedBy = p.ModifiedBy,
                        LastUpdateDate = p.LastUpdateDate.Value.ToShortDateString(),
                        Status = p.Status.ToString()
                    }).FirstOrDefaultAsync();

                    result.Data = permission;

                    if (permission != null)
                    {
                        return new Response<PermissionDto>(HttpStatusCode.OK, result.Data, "Successfully!", null);

                    }

                    return new Response<PermissionDto>(HttpStatusCode.NotFound, result.Data, "Not found data!", null);

                }
                catch (Exception ex)
                {
                    return new Response<PermissionDto>(HttpStatusCode.BadRequest, result.Data, $"bad request! {ex.Message}", null);

                }
            }
        }
    }
}
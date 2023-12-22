using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
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
    public class GetRoleByIdQuery : IRequest<Response<RoleDto>>
    {
        public string Id { get; set; }
        public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Response<RoleDto>>
        {
            private readonly IApplicationDbContext _context;
            public GetRoleByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
            {
                Response<RoleDto> result = new Response<RoleDto>();
                try
                {

                    RoleDto role = await _context.Roles.Where(x => x.Status == (int)StatusEnum.Active && x.Id.ToLower() == request.Id.ToLower()).Select(p => new RoleDto()
                    {
                        Id = p.Id.ToString(),
                        Name = CultureHelper.IsArabic ? p.NameAr : p.Name,
                        CreateBy = p.CreateBy,
                        CreationDate = p.CreationDate.ToShortDateString(),
                        ModifiedBy = p.ModifiedBy,
                        LastUpdateDate = p.LastUpdateDate.Value.ToShortDateString(),
                        Status = p.Status
                    }).FirstOrDefaultAsync();


                    result.Data = role;

                    if (role != null)
                    {

                        return new Response<RoleDto>(HttpStatusCode.OK, result.Data, "Successfully!", null);

                    }

                    return new Response<RoleDto>(HttpStatusCode.NotFound, result.Data, "Not found data!", null);

                }
                catch (Exception ex)
                {
                    return new Response<RoleDto>(HttpStatusCode.BadRequest, result.Data, $"bad request! {ex.Message}", null);

                }
            }
        }
    }
}
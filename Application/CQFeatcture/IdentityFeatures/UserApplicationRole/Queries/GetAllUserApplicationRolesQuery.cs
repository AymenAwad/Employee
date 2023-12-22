using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Persistence.Contexts;
using Shared.Wrappers;

namespace Application.Features.IdentityFeatures.UserApplicationRole.Queries
{
    public class GetAllUserApplicationRolesQuery : IRequest<Response<List<UserApplicationRoleDto>>>
    {
        public class GetAllUserApplicationRolesQueryHandler : IRequestHandler<GetAllUserApplicationRolesQuery, Response<List<UserApplicationRoleDto>>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllUserApplicationRolesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public Task<Response<List<UserApplicationRoleDto>>> Handle(GetAllUserApplicationRolesQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}

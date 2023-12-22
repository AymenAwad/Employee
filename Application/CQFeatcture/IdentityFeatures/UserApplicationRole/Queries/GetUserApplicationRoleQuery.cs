using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Persistence.Contexts;
using Shared.Wrappers;

namespace Application.Features.IdentityFeatures.UserApplicationRole.Queries
{
    public class GetUserApplicationRoleQuery : IRequest<Response<List<UserApplicationRoleDto>>>
    {
        public class GetUserApplicationRoleQueryHandler : IRequestHandler<GetUserApplicationRoleQuery, Response<List<UserApplicationRoleDto>>>
        {
            private readonly IApplicationDbContext _context;

            public GetUserApplicationRoleQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public Task<Response<List<UserApplicationRoleDto>>> Handle(GetUserApplicationRoleQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}

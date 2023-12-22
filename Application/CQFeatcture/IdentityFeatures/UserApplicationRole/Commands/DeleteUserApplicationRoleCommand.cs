using AutoMapper;
using MediatR;
using System.Net;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Application.Interfaces;
using Persistence.Contexts;
using Shared.Wrappers;
using System.Linq;

namespace Application.Features.IdentityFeatures.UserApplicationRole.Commands
{
    public class DeleteUserApplicationRoleCommand : DeleteUserApplicationRoleDto, IRequest<Response<int>>
    {

        public class DeleteUserApplicationRoleCommandHandler : IRequestHandler<DeleteUserApplicationRoleCommand, Response<int>>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationDbContext _context;
            private IUnitOfWork _unitOfWork { get; set; }

            public DeleteUserApplicationRoleCommandHandler(IMapper mapper, IApplicationDbContext context, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _context = context;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response<int>> Handle(DeleteUserApplicationRoleCommand request, CancellationToken cancellationToken)
            {
                Response<int> result = new Response<int>();
                try
                {
                    var userApplicationRole = _context.UserApplicationRoles.FirstOrDefault(s => s.Id == request.Id);

                    if (userApplicationRole != null)
                    {
                        request.ModifiedBy = "admin";
                        var data = _context.UserApplicationRoles.Remove(userApplicationRole);
                        await _unitOfWork.Commit(cancellationToken);

                        return new Response<int>(HttpStatusCode.Created, data.Entity.Id, "Successfully", null);
                    }

                    return new Response<int>(HttpStatusCode.NotFound, result.Data, "Try agin, Not update data!", null);
                }
                catch (Exception ex)
                {
                    return new Response<int>(HttpStatusCode.BadRequest, result.Data, ex.Message, null);
                }
            }
        }
    }
}

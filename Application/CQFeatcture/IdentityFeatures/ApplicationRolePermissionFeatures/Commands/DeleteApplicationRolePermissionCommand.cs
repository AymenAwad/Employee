using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Application.Interfaces;
using Persistence.Contexts;
using Shared.Wrappers;

namespace Tawteen.Application.Features.IdentityFeatures.ApplicationRolePermissionFeatures.Commands
{
    public class DeleteApplicationRolePermissionCommand : DeleteApplicationRolePermissionDto, IRequest<Response<int>>
    {

        public class DeleteApplicationRolePermissionCommandHandler : IRequestHandler<DeleteApplicationRolePermissionCommand, Response<int>>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationDbContext _context;
            private IUnitOfWork _unitOfWork { get; set; }

            public DeleteApplicationRolePermissionCommandHandler(IMapper mapper, IApplicationDbContext context, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _context = context;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response<int>> Handle(DeleteApplicationRolePermissionCommand request, CancellationToken cancellationToken)
            {
                Response<int> result = new Response<int>();
                try
                {                   
                    var  applicationRolePermission = _context.ApplicationRolePermissions.FirstOrDefault(s => s.Id == request.Id);

                    if (applicationRolePermission != null)
                    {
                        var data = _context.ApplicationRolePermissions.Remove(applicationRolePermission);
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

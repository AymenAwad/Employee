using AutoMapper;
using MediatR;
using System;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Application.Interfaces;
using Domain.Entities.Identity;
using Persistence.Contexts;
using Shared.Wrappers;

namespace Tawteen.Application.Features.IdentityFeatures.ApplicationRolePermissionFeatures.Commands
{
    public class CreateApplicationRolePermissionCommand : CreateApplicationRolePermissionDto, IRequest<Response<int>>
    {
        [JsonIgnore]
        public string CurrentUserId { get; set; } = "admin";

        public class CreateApplicationRolePermissionCommandHandler : IRequestHandler<CreateApplicationRolePermissionCommand, Response<int>>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationDbContext _context;
            private IUnitOfWork _unitOfWork { get; set; }

            public CreateApplicationRolePermissionCommandHandler(IMapper mapper, IApplicationDbContext context, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _context = context;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response<int>> Handle(CreateApplicationRolePermissionCommand request, CancellationToken cancellationToken)
            {
                Response<int> result = new Response<int>();
                
                try
                {
                    ApplicationRolePermission applicationRolePermission = _mapper.Map<ApplicationRolePermission>(request);
                    var data = await _context.ApplicationRolePermissions.AddAsync(applicationRolePermission);
                    await _unitOfWork.Commit(cancellationToken);

                    return new Response<int>(HttpStatusCode.Created, data.Entity.Id, "Successfully", null);
                }
                catch (Exception ex)
                {
                    return new Response<int>(HttpStatusCode.BadRequest, result.Data, ex.Message, null);
                }
            }
        }
    }
}

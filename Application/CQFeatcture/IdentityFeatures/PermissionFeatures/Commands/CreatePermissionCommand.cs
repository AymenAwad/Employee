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

namespace Tawteen.Application.Features.IdentityFeatures.PermissionFeatures.Commands
{
    public class CreatePermissionCommand : CreatePermissionDto, IRequest<Response<int>>
    {
        [JsonIgnore]
        public string CurrentUserId { get; set; } = "admin";

        public class CreatePermissionCommandHndler : IRequestHandler<CreatePermissionCommand, Response<int>>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationDbContext _context;
            private IUnitOfWork _unitOfWork { get; set; }

            public CreatePermissionCommandHndler(IMapper mapper, IApplicationDbContext context, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _context = context;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response<int>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
            {
                Response<int> result = new Response<int>();

                try
                {
                    request.CreateBy = "admin"; // request.CurrentUserId;
                    request.CreationDate = DateTime.Now;
                    Permission permission = _mapper.Map<Permission>(request);
                    var data = await _context.Permissions.AddAsync(permission);
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

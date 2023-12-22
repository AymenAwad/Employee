using AutoMapper;
using MediatR;
using System;
using System.Linq;
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
    public class DeletePermissionCommand : DeletePermissionDto, IRequest<Response<int>>
    {
        [JsonIgnore]
        public string CurrentUserId { get; set; } = "admin";
    }

    public class DeletePermissionCommandHndler : IRequestHandler<DeletePermissionCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private IUnitOfWork _unitOfWork { get; set; }

        public DeletePermissionCommandHndler(IMapper mapper, IApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            Response<int> result = new Response<int>();

            try
            {
                Permission permission = _context.Permissions.FirstOrDefault(s => s.Id == request.Id );

                if (permission != null)
                {
                    permission.Status = request.Status;
                    permission.CreateBy = (!string.IsNullOrWhiteSpace(permission.CreateBy)) ? permission.CreateBy : "admin";
                    permission.ModifiedBy = request.CurrentUserId;
                    permission.LastUpdateDate = request.LastUpdateDate;

                    var data = _context.Permissions.Update(permission);
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

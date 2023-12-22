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
using Domain.Enum;
using Persistence.Contexts;
using Shared.Wrappers;

namespace Application.Features.IdentityFeatures.RoleFeatures.Commands
{
    public class DeleteRoleCommand : DeleteRoleDto, IRequest<Response<string>>
    {
        [JsonIgnore]
        public string CurrentUserId { get; set; } = "admin";
    }

    public class DeletePermissionCommandHndler : IRequestHandler<DeleteRoleCommand, Response<string>>
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

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            Response<int> result = new Response<int>();

            try
            {
                Role role = _context.Roles.FirstOrDefault(s => s.Id.ToLower() == request.Id.ToLower());

                if (role != null)
                {
                    role.Status = (int)StatusEnum.Deleted;                    
                    role.ModifiedBy = request.CurrentUserId;
                    role.LastUpdateDate = request.LastUpdateDate;

                    var data = _context.Roles.Update(role);
                    await _unitOfWork.Commit(cancellationToken);

                    return new Response<string>(HttpStatusCode.Created, data.Entity.Id, "Successfully", null);
                }

                return new Response<string>(HttpStatusCode.NotFound, null, "Try agin, Not update data!", null);
            }
            catch (Exception ex)
            {
                return new Response<string>(HttpStatusCode.BadRequest, null, ex.Message, null);
            }
        }
    }
}

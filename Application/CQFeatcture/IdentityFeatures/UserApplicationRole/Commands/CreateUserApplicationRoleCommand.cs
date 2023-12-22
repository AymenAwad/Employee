using AutoMapper;
using MediatR;
using System;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Application.Interfaces;
using Persistence.Contexts;
using Shared.Wrappers;
using Domain.Entities.Identity;

namespace Application.Features.IdentityFeatures.UserApplicationRole.Commands
{
    public class CreateUserApplicationRoleCommand : CreateUserApplicationRoleDto, IRequest<Response<int>>
    {
        [JsonIgnore]
        public string CurrentUserId { get; set; } = "admin";

        public class CreateUserApplicationRoleCommandHandler : IRequestHandler<CreateUserApplicationRoleCommand, Response<int>>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationDbContext _context;
            private IUnitOfWork _unitOfWork { get; set; }

            public CreateUserApplicationRoleCommandHandler(IMapper mapper, IApplicationDbContext context, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _context = context;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response<int>> Handle(CreateUserApplicationRoleCommand request, CancellationToken cancellationToken)
            {
                Response<int> result = new Response<int>();

                try
                {
                    request.CreateBy = request.CurrentUserId;
                    Domain.Entities.Identity.UserApplicationRole userApplicationRole = _mapper.Map<Domain.Entities.Identity.UserApplicationRole>(request);
                    var data = await _context.UserApplicationRoles.AddAsync(userApplicationRole);
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

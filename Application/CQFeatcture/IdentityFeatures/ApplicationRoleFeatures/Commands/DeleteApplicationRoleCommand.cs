using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Application.Interfaces;
using Domain.Entities.Identity;
using Domain.Enum;
using Persistence.Contexts;
using Shared.Wrappers;

namespace Application.Features.IdentityFeatures.ApplicationRoleFeatures.Commands
{
    public class DeleteApplicationRoleCommand : DeleteApplicationRoleDto, IRequest<Response<int>>
    {

        public class DeleteApplicationRoleCommandHndler : IRequestHandler<DeleteApplicationRoleCommand, Response<int>>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationDbContext _context;
            private IUnitOfWork _unitOfWork { get; set; }

            public DeleteApplicationRoleCommandHndler(IMapper mapper, IApplicationDbContext context, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _context = context;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response<int>> Handle(DeleteApplicationRoleCommand request, CancellationToken cancellationToken)
            {
                Response<int> result = new Response<int>();

                try
                {
                    ApplicationRole applicationRole = new ApplicationRole();

                    if (request.ApplicationTypeId == (int)ApplicationTypeEnum.Employee)
                    {
                        applicationRole = _context.ApplicationRole.FirstOrDefault(s => s.Id == request.Id && s.EmployeeId == request.ApplicationId);
                    }
                    if (applicationRole != null)
                    {
                        var data = _context.ApplicationRole.Remove(applicationRole);
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
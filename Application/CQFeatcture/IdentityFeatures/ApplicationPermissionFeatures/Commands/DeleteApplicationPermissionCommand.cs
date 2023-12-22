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

namespace Application.Features.IdentityFeatures.ApplicationPermissionFeatures.Commands
{
    public class DeleteApplicationPermissionCommand : DeleteApplicationPermissionDto, IRequest<Response<int>>
    {

        public class DeleteApplicationPermissionCommandHndler : IRequestHandler<DeleteApplicationPermissionCommand, Response<int>>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationDbContext _context;
            private IUnitOfWork _unitOfWork { get; set; }

            public DeleteApplicationPermissionCommandHndler(IMapper mapper, IApplicationDbContext context, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _context = context;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response<int>> Handle(DeleteApplicationPermissionCommand request, CancellationToken cancellationToken)
            {
                Response<int> result = new Response<int>();

                try
                {
                    ApplicationPermission applicationPermission = new ApplicationPermission();

                    if (request.ApplicationTypeId == (int)ApplicationTypeEnum.Employee)
                    {
                        applicationPermission = _context.ApplicationPermissions.FirstOrDefault(s => s.Id == request.Id && s.EmployeeId == request.ApplicationId);
                    }

                    if (applicationPermission != null)
                    {
                        var data = _context.ApplicationPermissions.Remove(applicationPermission);
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
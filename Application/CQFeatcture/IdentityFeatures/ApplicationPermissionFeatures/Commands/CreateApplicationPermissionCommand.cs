using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Dynamic.Core;
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
    public class CreateApplicationPermissionCommand : CreateApplicationPermissionDto, IRequest<Response<int>>
    {
        [JsonIgnore]
        public string CurrentUserId { get; set; } = "admin";

        public class CreateApplicationPermissionCommandHndler : IRequestHandler<CreateApplicationPermissionCommand, Response<int>>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationDbContext _context;
            private IUnitOfWork _unitOfWork { get; set; }

            public CreateApplicationPermissionCommandHndler(IMapper mapper, IApplicationDbContext context, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _context = context;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response<int>> Handle(CreateApplicationPermissionCommand request, CancellationToken cancellationToken)
            {
                Response<int> result = new Response<int>();


                try
                {
                    ApplicationPermission applicationPermission = new ApplicationPermission();

                    if (request.ApplicationTypeId == (int)ApplicationTypeEnum.Employee)
                    {
                        applicationPermission = await _context.ApplicationPermissions.FirstOrDefaultAsync(a => a.EmployeeId == request.ApplicationId && a.PermissionId == request.PermissionId);

                        if (applicationPermission == null)
                        {
                            applicationPermission = new ApplicationPermission()
                            {
                                EmployeeId = request.ApplicationId,
                                PermissionId = request.PermissionId
                            };
                               
                            var data = await _context.ApplicationPermissions.AddAsync(applicationPermission);
                            await _unitOfWork.Commit(cancellationToken);
                            return new Response<int>(HttpStatusCode.Created, data.Entity.Id, "Successfully", null);
                        }
                    }
                   

                    return new Response<int>(HttpStatusCode.Found, applicationPermission.Id, "The item already exists", null);

                }
                catch (Exception ex)
                {
                    return new Response<int>(HttpStatusCode.BadRequest, result.Data, ex.Message, null);
                }
            }
        }
    }
}

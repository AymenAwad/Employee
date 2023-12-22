using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Wrappers;
using System;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Application.Interfaces;
using Domain.Entities.Identity;
using Domain.Enum;
using Persistence.Contexts;

namespace Application.Features.IdentityFeatures.ApplicationRoleFeatures.Commands
{
    public class CreateApplicationRoleCommand : CreateApplicationRoleDto, IRequest<Response<int>>
    {
        [JsonIgnore]
        public string CurrentUserId { get; set; } = "admin";

        public class CreateApplicationRoleCommandHndler : IRequestHandler<CreateApplicationRoleCommand, Response<int>>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationDbContext _context;
            private IUnitOfWork _unitOfWork { get; set; }

            public CreateApplicationRoleCommandHndler(IMapper mapper, IApplicationDbContext context, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _context = context;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response<int>> Handle(CreateApplicationRoleCommand request, CancellationToken cancellationToken)
            {
                Response<int> result = new Response<int>();


                try
                {
                    ApplicationRole applicationRole = new ApplicationRole();

                    if (request.ApplicationTypeId == (int)ApplicationTypeEnum.Employee)
                    {
                        applicationRole = await _context.ApplicationRole.FirstOrDefaultAsync(a => a.EmployeeId == request.ApplicationId && a.RoleId == request.RoleId);

                        if (applicationRole == null)
                        {
                            applicationRole = new ApplicationRole();
                            applicationRole.EmployeeId = request.ApplicationId;
                            applicationRole.RoleId = request.RoleId;
                            applicationRole.CreateBy = request.CreateBy;
                            applicationRole.CreationDate = DateTime.Now;
                            applicationRole.Status = (int)StatusEnum.Active;

                            var data = await _context.ApplicationRole.AddAsync(applicationRole);
                            await _unitOfWork.Commit(cancellationToken);
                            return new Response<int>(HttpStatusCode.Created, data.Entity.Id, "Successfully", null);
                        }
                    }

                    return new Response<int>(HttpStatusCode.Found, applicationRole.Id, "The item already exists", null);

                }
                catch (Exception ex)
                {
                    return new Response<int>(HttpStatusCode.BadRequest, result.Data, ex.Message, null);
                }
            }
        }
    }
}

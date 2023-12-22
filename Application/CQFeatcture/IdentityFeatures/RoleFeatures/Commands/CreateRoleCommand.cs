using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Application.Interfaces;
using Domain.Entities.Identity;
using Persistence.Contexts;
using Shared.Wrappers;
using Shared.Exceptions;

namespace Application.Features.IdentityFeatures.RoleFeatures.Commands
{
    public class CreateRoleCommand : CreateRoleDto, IRequest<Response<string>>
    {
        public class CreateRoleCommandHndler : IRequestHandler<CreateRoleCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IApplicationDbContext _context;
            private IUnitOfWork _unitOfWork { get; set; }

            public CreateRoleCommandHndler(IMapper mapper, IApplicationDbContext context, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _context = context;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
            {
                Response<int> result = new Response<int>();                               

                //// creating Creating Employee role     
                //bool x = await _roleManager.RoleExistsAsync(request.Name);
                //if (!x)
                //{
                //    var role = new IdentityRole();
                //    role.Name = request.Name;
                    
                //    await _roleManager.CreateAsync(role);
                //}

                try
                {
                    request.CreateBy = "admin"; // request.CurrentUserId;
                    request.CreationDate = DateTime.Now;
                    request.NormalizedName= request.Name.ToUpperInvariant();
                    request.ConcurrencyStamp= Guid.NewGuid().AsSequentialGuid().ToString();

                    Role role = _mapper.Map<Role>(request);
                    var data = await _context.Roles.AddAsync(role);
                    await _unitOfWork.Commit(cancellationToken);

                    return new Response<string>(HttpStatusCode.Created, data.Entity.Id, "Successfully", null);
                }
                catch (Exception ex)
                {
                    return new Response<string>(HttpStatusCode.BadRequest, null, $"bad request {ex.Message}", null);
                }
            }
        }
    }
}
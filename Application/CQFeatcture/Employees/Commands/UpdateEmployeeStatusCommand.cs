using Application.Dtos.EmployeeDto;
using Application.Interface.Interfaces;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Application;
using MediatR;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.EntityFrameworkCore;
using Shared.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.CQFeatcture.Employees.Commands
{
    public class UpdateEmployeeStatusCommand : UpdateEmployeeStatusDto, IRequest<Response<int>>
    {
        [JsonIgnore]
        public string CurrentUserId { get; set; } = "admin";
    }

    public class UpdateEmployeeStatusCommandHandler : IRequestHandler<UpdateEmployeeStatusCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly IEmployee _employee;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateEmployeeStatusCommandHandler(IMapper mapper, IEmployee employee, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _employee = employee;
            _unitOfWork = unitOfWork;
        }


        public async Task<Response<int>> Handle(UpdateEmployeeStatusCommand request, CancellationToken cancellationToken)
        {
            Response<int> result = new Response<int>();

            try
            {
                Employee restaurant = await _employee.GetByQuery(s => s.Id == request.Id).FirstOrDefaultAsync();

                if (restaurant != null)
                {
                    restaurant.ModifiedBy = request.CurrentUserId;
                    restaurant.LastUpdateDate = DateTime.Now;
                    restaurant.Status = request.Status;


                    Employee data = await _employee.UpdateAsync(restaurant);
                    await _unitOfWork.Commit(cancellationToken);

                    return new Response<int>(HttpStatusCode.Created, data.Id, "Successfully", null);
                }

                return new Response<int>(HttpStatusCode.NotFound, result.Data, "Try agin, Not update data!", null);

            }
            catch (Exception ex)
            {
                return new Response<int>(HttpStatusCode.BadRequest, result.Data, $"Bad Request: {ex.Message}", null);

            }
        }
    }
}

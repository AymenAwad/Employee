using Application.Dtos.EmployeeDto;
using Application.Interface.Interfaces;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Application;
using Domain.Enum;
using MediatR;
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
    public class UpdateEmployeeCommand : UpdateEmployeeDto, IRequest<Response<int>>
    {
        [JsonIgnore]
        public string CurrentUserId { get; set; } = "admin";
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly IEmployee _employee;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateEmployeeCommandHandler(IMapper mapper, IEmployee employee, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _employee = employee;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Response<int> result = new Response<int>();

            try
            {
                Employee employeeObj = await _employee.GetByQuery(s => s.Id == request.Id && s.Status == (int)StatusEnum.Active).FirstOrDefaultAsync();

                if (employeeObj != null)
                {
                    request.CreateBy = employeeObj.CreateBy;
                    request.CreationDate = Convert.ToDateTime(employeeObj.CreationDate);
                    request.ModifiedBy = request.CurrentUserId;
                    request.LastUpdateDate = DateTime.Now;
                    Employee employee = _mapper.Map<Employee>(request);
                    Employee data = await _employee.UpdateAsync(employee);
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

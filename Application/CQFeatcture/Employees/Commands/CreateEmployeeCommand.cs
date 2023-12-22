using Application.Interface.Interfaces;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Application;
using MediatR;
using Shared.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Application.CQFeatcture.Employees.Commands
{
    public class CreateEmployeeCommand : Employee, IRequest<Response<int>>
    {
    }

    public class CreateEmployeeCommandHandle : IRequestHandler<CreateEmployeeCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        private readonly IEmployee _employee;

        public CreateEmployeeCommandHandle(IMapper mapper, IUnitOfWork unitOfWork, IEmployee employee)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _employee = employee;
        }

        public async Task<Response<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Response<int> result = new Response<int>();

            try
            {
                request.LastUpdateDate = DateTime.Now;
                request.CreateBy = "Admin";
                Employee employee = _mapper.Map<Employee>(request);
                Employee Data = await _employee.AddAsync(employee);
                await _unitOfWork.Commit(cancellationToken);
                result.Data = Data.Id;
                result.HttpStatusCode = HttpStatusCode.OK;
                result.Succeeded = true;
                result.Message = "Succeafully";

            }
            catch (Exception ex)
            {
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                result.Succeeded = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}


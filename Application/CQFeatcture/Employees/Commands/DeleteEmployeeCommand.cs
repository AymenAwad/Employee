using Application.Interface.Interfaces;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Application;
using Domain.Enum;
using MediatR;
using Shared.Wrappers;
using System.Net;

namespace Application.CQFeatcture.Employees.Commands
{
    public class DeleteEmployeeCommand : IRequest<Response<int>>
    {
        public string Id { get; set; }
        public string UserId { get; set; } = "admin";
    }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly IEmployee _employee;
        private IUnitOfWork _unitOfWork { get; set; }
        public DeleteEmployeeCommandHandler(IMapper mapper, IEmployee employee, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _employee = employee;
            _unitOfWork = unitOfWork;
        }


        public async Task<Response<int>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            Response<int> result = new Response<int>();

            try
            {
                var employeeObj = _employee.GetByQuery(x => x.Id == int.Parse(request.Id)).FirstOrDefault();

                employeeObj.Status = (int)StatusEnum.Deleted;
                employeeObj.ModifiedBy = request.UserId;
                employeeObj.LastUpdateDate = DateTime.Now.Date;

                Employee employee = _mapper.Map<Employee>(employeeObj);
                Employee data = await _employee.UpdateAsync(employee);
                await _unitOfWork.Commit(cancellationToken);
                result.Data = data.Id;
                result.HttpStatusCode = HttpStatusCode.OK;
                result.Succeeded = true;
                result.Message = "Successfully Delete!";
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

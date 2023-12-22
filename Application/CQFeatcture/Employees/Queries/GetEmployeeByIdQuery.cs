using Application.Dtos.EmployeeDto;
using Application.Interface.Interfaces;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Globalization;
using Shared.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQFeatcture.Employees.Queries
{
    public class GetEmployeeByIdQuery : IRequest<Response<EmployeeDto>>
    {
        public string Id { get; set; }
    }
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Response<EmployeeDto>>
    {
        private readonly IEmployee _employee;

        public GetEmployeeByIdQueryHandler(IEmployee employee)
        {
            _employee = employee;
        }
        public async Task<Response<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            Response<EmployeeDto> result = new Response<EmployeeDto>();

            try
            {
                EmployeeDto Employee = await _employee.GetByQuery(a => a.Status == (int)StatusEnum.Active && a.Id.ToString().ToLower() == request.Id.ToLower())
                                               .Select(a => new EmployeeDto
                                               {
                                                   EmployeeName = a.Name,
                                                   Agency = a.Agency,
                                                   Department = a.Department,
                                                   Rating = a.Rating,
                                                   Year = a.Year,
                                                   Category = a.Category,
                                                   CreationDate = a.CreationDate.Date,
                                                   ModifiedBy = a.ModifiedBy,
                                                   LastUpdateDate = a.LastUpdateDate.Value.Date,
                                                   Status = a.Status.ToString(),
                                               }).FirstOrDefaultAsync();

                result.Data = Employee;

                if (Employee != null)
                {
                    return new Response<EmployeeDto>(HttpStatusCode.OK, result.Data, "The result is seccessfully!", null);
                }

                return new Response<EmployeeDto>(HttpStatusCode.NotFound, result.Data, "The result is not found!", null);
            }
            catch (Exception ex)
            {
                return new Response<EmployeeDto>(HttpStatusCode.BadRequest, result.Data, $"Bad request: {ex.Message}", null);
            }
        }
    }
}

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
    public class GetAllEmployeeQuery : IRequest<Response<List<EmployeeDto>>>
    {
    }
    public class GetAllEmploeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, Response<List<EmployeeDto>>>
    {
        private readonly IEmployee _employee;


        public GetAllEmploeeQueryHandler(IEmployee employee)
        {
            _employee = employee;
        }

        public async Task<Response<List<EmployeeDto>>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<EmployeeDto> employees = await _employee.GetByQuery(a => a.Status == (int)StatusEnum.Active).Select(a => new EmployeeDto()
                {
                    Id = a.Id.ToString(),
                    EmployeeName = a.Name,
                    Agency = a.Agency,
                    Department = a.Department,
                    Rating = a.Rating,
                    Year = a.Year,
                    Category = a.Category,
                    CreateBy = a.CreateBy,
                    CreationDate = a.CreationDate.Date,
                    ModifiedBy = a.ModifiedBy,
                    LastUpdateDate = a.LastUpdateDate.Value.Date,
                    Status = a.Status.ToString(),
                  
                }).ToListAsync();

                if (employees != null)
                    return new Response<List<EmployeeDto>>(HttpStatusCode.OK, employees.ToList(), "seccessfully!", null);
                return new Response<List<EmployeeDto>>(HttpStatusCode.NotFound, employees.ToList(), "not found data!", null);
            }
            catch (Exception ex)
            {
                return new Response<List<EmployeeDto>>(HttpStatusCode.BadRequest, null, $"bad request: {ex.Message}", null);

            }
        }
    }


}






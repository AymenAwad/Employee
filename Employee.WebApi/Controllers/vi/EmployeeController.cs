using Application.CQFeatcture.Employees.Commands;
using Application.CQFeatcture.Employees.Queries;
using Application.Dtos.EmployeeDto;
using Domain.Entities.Application;
using Employee.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Employee.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}")]
    public class EmployeeController : BaseApiController
    {
        [HttpGet("employees/all")]
        public async Task<IActionResult> GetAllEmployeeIdQuery()
        {
            return Ok(await Mediator.Send(new GetAllEmployeeQuery()));
        }


        [HttpGet("employees/{employeeId}")]
        public async Task<IActionResult> Employees(string employeeId)
        {
            return Ok(await Mediator.Send(new GetEmployeeByIdQuery() { Id = employeeId }));
        }

        [HttpDelete("employees/{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(string employeeId)
        {
            return Ok(await Mediator.Send(new DeleteEmployeeCommand() { Id = employeeId }));
        }

        [HttpPost("employees")]
        public async Task<IActionResult> Employee(CreateEmployeeDto model)
        {
            return Ok(await Mediator.Send(new CreateEmployeeCommand()
            {
                Name = model.EmployeeName,
                Agency = model.Agency,
                Department = model.Department,
                Rating = model.Rating,
                Year = model.Year,
                Category = model.Category,
                Status = model.Status
            }));
        }

        [HttpPut("employee/{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, UpdateEmployeeCommand model)
        {
            model.Id = employeeId;
            return Ok(await Mediator.Send(model));
        }

        [HttpPut("employee-status/{employeeId}")]
        public async Task<IActionResult> UpdateEmployeeStatusCommand(int employeeId, UpdateEmployeeStatusCommand model)
        {
            model.Id = employeeId;
            return Ok(await Mediator.Send(model));
        }
    }
}

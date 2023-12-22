using Application.Dtos.EmployeeDto;
using Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.AuthDto
{
    public class RegisterEmployeeDto
    {
        public RegisterRequest RegisterRequest { get; set; }
        public CreateEmployeeDto CreateEmployee { get; set; }
    }
}

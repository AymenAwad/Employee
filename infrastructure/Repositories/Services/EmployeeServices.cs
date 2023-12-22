using Application.Interface.Interfaces;
using Domain.Entities.Application;
using Infrastructure.Implementation;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Services
{
    public class EmployeeServices : GenericRepository<Employee> , IEmployee
    {
        private readonly DbSet<Employee> _context;

        public EmployeeServices(ApplicationDbContext context) : base(context) 
        {
            _context = context.Set<Employee>();
        }
    }
}

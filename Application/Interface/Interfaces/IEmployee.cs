using Application.Interfaces;
using Domain.Entities.Application;

namespace Application.Interface.Interfaces
{
    public interface IEmployee : IGenericRepository<Employee>
    {
    }
}

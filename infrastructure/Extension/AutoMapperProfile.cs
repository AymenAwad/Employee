using AutoMapper;
using Application.Dtos;
using Application.Dtos.Identity;
using Domain.Entities.Identity;
using Application.Dtos.EmployeeDto;
using Domain.Entities.Application;

namespace Infrastructure.Extension
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Application Mapper
            CreateMap<Employee, CreateEmployeeDto>();
            CreateMap<EmployeeDto, Employee>().ReverseMap();

            #endregion

            #region Identity            
            CreateMap<Role, CreateRoleDto>().ReverseMap();

            CreateMap<Permission, CreatePermissionDto>().ReverseMap();

            CreateMap<ApplicationRole, CreateApplicationRoleDto>().ReverseMap();

            CreateMap<ApplicationPermission, CreateApplicationPermissionDto>().ReverseMap();

            CreateMap<UserApplicationRole, CreateUserApplicationRoleDto>().ReverseMap();
            
            CreateMap<ApplicationRolePermission, CreateApplicationRolePermissionDto>().ReverseMap();
            #endregion

        }
    }
}

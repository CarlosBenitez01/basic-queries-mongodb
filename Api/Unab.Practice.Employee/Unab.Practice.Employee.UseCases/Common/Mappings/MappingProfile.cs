using AutoMapper;
using Unab.Practice.Employees.Domain.Entities;
using Unab.Practice.Employees.Dto.Dtos;
using Unab.Practice.Employees.Dto.Enums;
using Unab.Practice.Employees.UseCases.Employees.Commands.CreateEmployeeCommand;
using Unab.Practice.Employees.UseCases.Employees.Commands.UpdateEmployeeCommand;

namespace Unab.Practice.Employees.UseCases.Common.Mappings
{
    internal class MappingProfile: Profile
    {
        public MappingProfile()
        {

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ReverseMap();

            CreateMap<Employee, CreateEmployeeCommand>()
                .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => (SexDto)src.Sex))
                .ReverseMap();
            CreateMap<Employee, UpdateEmployeeCommand>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => (SexDto)src.Sex))
                .ReverseMap();
        }
    }
}

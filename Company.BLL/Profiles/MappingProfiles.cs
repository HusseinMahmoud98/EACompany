using AutoMapper;
using Company.DAL.Entities;
using Company.Shared.Dtos.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
            //.ForMember(dest => dest.DepartmentId,
            //           opt => opt.MapFrom<DepartmentResolver, string>(src => src.Department));
            CreateMap<DetailsEmployeeDto, Employee>().ReverseMap();

        }
    }
}

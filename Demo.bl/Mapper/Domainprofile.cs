using AutoMapper;
using DemoKhaled.BL.Models;
using DemoKhaled.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.Mapper
{
    public class DomainProfile: Profile
    {
        public DomainProfile()
        {
            CreateMap<deptVm, Department>();
            CreateMap<Department, deptVm>();
            CreateMap<EmployeeVm, Employee>();
            CreateMap<Employee, EmployeeVm>();

            CreateMap<CountryVm, Country>();
            CreateMap<Country, CountryVm>();
            CreateMap<CityVM, City>();
            CreateMap<City, CityVM>();
            CreateMap<District, DistinctVM>();
            CreateMap<DistinctVM, District>();
        }
    }
}

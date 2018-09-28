using AutoMapper;
using dotnet_test.Models;
using dotnet_test.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Medic, MedicViewModel>();
            CreateMap<MedicViewModel, Medic>();
        }
    }
}

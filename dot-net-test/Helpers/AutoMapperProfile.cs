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
            CreateMap<Medic, MedicScheduleViewModel>();
            CreateMap<MedicScheduleViewModel, Medic>();

            CreateMap<Patient, PatientViewModel>();
            CreateMap<PatientViewModel, Patient>();
            CreateMap<Patient, PatientScheduleViewModel>();
            CreateMap<PatientScheduleViewModel, Patient>();

            CreateMap<SystemUser, SystemUserViewModel>();
            CreateMap<SystemUserViewModel, SystemUser>();

            CreateMap<Medicine, MedicineViewModel>();
            CreateMap<MedicineViewModel, Medicine>();

            CreateMap<ScheduleTreatment, ScheduleTreatmentViewModel>();
            CreateMap<ScheduleTreatmentViewModel, ScheduleTreatment>();
            CreateMap<ScheduleTreatment, ScheduleTreatmentResultViewModel>();
            CreateMap<ScheduleTreatmentResultViewModel, ScheduleTreatment>();

            CreateMap<MedicineScheduleTreatment, MedicineScheduleTreatmentViewModel>();
            CreateMap<MedicineScheduleTreatmentViewModel, MedicineScheduleTreatment>();

            CreateMap<MedicineScheduleTreatment, MedicineScheduleTreatmentViewModel>();
            CreateMap<MedicineScheduleTreatmentViewModel, MedicineScheduleTreatment>();

            CreateMap<Treatment, TreatmentViewModel>();
            CreateMap<TreatmentViewModel, Treatment>();
        }
    }
}

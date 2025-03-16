using AutoMapper;
using TireApi.EfCore;
using TireApi.Models;

namespace TireApi.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Appointment, AppointmentModel>()
            .ForMember(dest => dest.ServiceTypeIds, opt => opt.MapFrom(src => src.AppointmentServiceTypes.Select(ast => ast.ServiceTypeId).ToList()))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AppointmentModel, Appointment>()
                .ForMember(dest => dest.AppointmentServiceTypes, opt => opt.MapFrom(src => src.ServiceTypeIds.Select(id => new AppointmentServiceType { ServiceTypeId = id })));
            CreateMap<Car, CarModel>().ReverseMap();
            CreateMap<Client, ClientModel>().ReverseMap();
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<ServiceType, ServiceTypeModel>().ReverseMap();
        }
    }
}

using AutoMapper;
using TireApi.EfCore;
using TireApi.Models;

namespace TireApi.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Appointment, AppointmentModel>().ReverseMap();
            CreateMap<Car, CarModel>().ReverseMap();
            CreateMap<Client, ClientModel>().ReverseMap();
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<ServiceType, ServiceTypeModel>().ReverseMap();
        }
    }
}

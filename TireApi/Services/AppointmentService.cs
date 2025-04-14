using AutoMapper;
using TireApi.EfCore;
using TireApi.Models;
using TireApi.Repositories.Interfaces;
using TireApi.Services.Interfaces;
using TireApi.Common;

namespace TireApi.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> GetAppointmentsAsync()
        {
            var appointments = await _appointmentRepository.GetAppointmentsAsync();
            var mappedAppointments = _mapper.Map<List<AppointmentDetailsModel>>(appointments);
            return mappedAppointments.Any()
                ? ResponseHandler.GetAppResponse(ResponseType.Success, mappedAppointments)
                : ResponseHandler.GetAppResponse(ResponseType.NotFound, "No appointments found");
        }

        public async Task<ApiResponse> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            return appointment != null
                ? ResponseHandler.GetAppResponse(ResponseType.Success, _mapper.Map<AppointmentDetailsModel>(appointment))
                : ResponseHandler.GetAppResponse(ResponseType.NotFound, "Appointment not found");
        }

        public async Task<ApiResponse> CreateAppointmentAsync(AppointmentModel appointmentModel)
        {
            if (appointmentModel == null)
                return ResponseHandler.GetAppResponse(ResponseType.Failure, "Invalid appointment data");

            var createdAppointment = await _appointmentRepository.CreateAppointmentAsync(_mapper.Map<Appointment>(appointmentModel));
            return ResponseHandler.GetAppResponse(ResponseType.Created, _mapper.Map<AppointmentModel>(createdAppointment));
        }

        public async Task<ApiResponse> UpdateAppointmentAsync(AppointmentModel appointmentModel)
        {
            var updatedAppointment = await _appointmentRepository.UpdateAppointmentAsync(_mapper.Map<Appointment>(appointmentModel));
            return updatedAppointment != null
                ? ResponseHandler.GetAppResponse(ResponseType.Success, _mapper.Map<AppointmentModel>(updatedAppointment))
                : ResponseHandler.GetAppResponse(ResponseType.NotFound, "Appointment not found");
        }

        public async Task<ApiResponse> DeleteAppointmentAsync(int appointmentId)
        {
            return await _appointmentRepository.DeleteAppointmentAsync(appointmentId)
                ? ResponseHandler.GetAppResponse(ResponseType.Deleted, "Appointment deleted")
                : ResponseHandler.GetAppResponse(ResponseType.NotFound, "Appointment not found");
        }
    }
}

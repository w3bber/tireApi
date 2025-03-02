using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TireApi.Models
{
    public class AppointmentServiceTypeModel
    {
        public int AppointmentId { get; set; }

        public int ServiceTypeId { get; set; }

        public AppointmentModel? Appointment { get; set; }

        public ServiceTypeModel? ServiceType { get; set; }
    }
}
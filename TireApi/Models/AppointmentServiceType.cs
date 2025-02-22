using System.ComponentModel.DataAnnotations.Schema;

namespace TireApi.Models
{
    public class AppointmentServiceType
    {
        public int AppointmentId { get; set; }
        public int ServiceTypeId { get; set; }

        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }

        [ForeignKey("ServiceTypeId")]
        public ServiceType ServiceType { get; set; }
    }
}
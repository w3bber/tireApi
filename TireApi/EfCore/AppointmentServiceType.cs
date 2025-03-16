using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TireApi.EfCore
{
    [PrimaryKey(nameof(AppointmentId), nameof(ServiceTypeId))]
    public class AppointmentServiceType
    {
        public int AppointmentId { get; set; }

        public int ServiceTypeId { get; set; }

        [ForeignKey("AppointmentId")]
        public Appointment? Appointment { get; set; }

        [ForeignKey("ServiceTypeId")]
        public ServiceType? ServiceType { get; set; }
    }
}
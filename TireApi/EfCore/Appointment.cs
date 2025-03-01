using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TireApi.EfCore
{
    public class Appointment
    {
        [Key, Required]
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = string.Empty;

        // Внешние ключи
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public int CarId { get; set; }
        [ForeignKey("CarId")]
        public Car Car { get; set; }
        public ICollection<AppointmentServiceType> AppointmentServiceTypes { get; set; }
    }
}
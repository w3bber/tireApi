using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TireApi.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
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
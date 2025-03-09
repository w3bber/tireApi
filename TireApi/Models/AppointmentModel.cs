using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TireApi.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = string.Empty;
        public int EmployeeId { get; set; }
        public int CarId { get; set; }
        public List<int> ServiceTypeIds { get; set; } = new();
    }
}
using System.ComponentModel.DataAnnotations;

namespace TireApi.EfCore
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public ICollection<Appointment> Appointments { get; set; }
    }
}

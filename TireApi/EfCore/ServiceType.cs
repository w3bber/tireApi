using System.ComponentModel.DataAnnotations;

namespace TireApi.EfCore
{
    public class ServiceType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Duration { get; set; } // в минутах
        public ICollection<AppointmentServiceType>? AppointmentServiceTypes { get; set; }
    }
}

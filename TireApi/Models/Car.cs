using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TireApi.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public float TireSize { get; set; }
        public int Year { get; set; }
        public string Numbers { get; set; } = string.Empty; // номера автомобиля

        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client? Client { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
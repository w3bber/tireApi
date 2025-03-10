using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TireApi.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public decimal TireSize { get; set; }
        public int Year { get; set; }
        public string Numbers { get; set; } = string.Empty; // номера автомобиля
        public int ClientId { get; set; }
        public List<int>? AppointmentIds { get; set; }

        public CarModel()
        {
            
        }

        public CarModel(int id, string brand, string model, decimal tireSize, int year, string numbers, int clientId, List<int>? appointmentIds)
        {
            Id = id;
            Brand = brand;
            Model = model;
            TireSize = tireSize;
            Year = year;
            Numbers = numbers;
            ClientId = clientId;
            AppointmentIds = appointmentIds;
        }


    }
}
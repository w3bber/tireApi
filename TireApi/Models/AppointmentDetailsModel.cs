namespace TireApi.Models
{
    public class AppointmentDetailsModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = string.Empty;
        public CarModel Car { get; set; } = new();
        public EmployeeModel Employee { get; set; } = new();
        public List<ServiceTypeModel> ServiceTypes { get; set; } = new();
    }
}

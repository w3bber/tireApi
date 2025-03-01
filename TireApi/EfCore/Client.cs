using System.ComponentModel.DataAnnotations;

namespace TireApi.EfCore
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public ICollection<Car> Cars { get; set; } // 1 : N
    }
}

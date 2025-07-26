
namespace Domain.Models
{
    public class Customer : BaseEntity
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;


        // Navigation properties
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
      
    }
}

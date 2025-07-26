
using Domain.Models.OrderModels;

namespace Domain.Models
{
    public class Order : BaseEntity
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        // Navigation properties
        public virtual Customer Customer { get; set; } = null!;
        public virtual IEnumerable<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual Invoice Invoice { get; set; }
    }
}

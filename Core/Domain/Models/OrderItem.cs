
namespace Domain.Models
{
    public class OrderItem : BaseEntity
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; } = 0;


        // Navigation properties
        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;

        // Calculated property
        public decimal TotalPrice => Quantity * UnitPrice * (1 - Discount);
    }
}

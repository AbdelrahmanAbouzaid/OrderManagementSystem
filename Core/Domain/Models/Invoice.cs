

namespace Domain.Models
{
    public class Invoice : BaseEntity
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }


        // Navigation properties
        public virtual Order Order { get; set; } = null!;
    }
}

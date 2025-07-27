

namespace Shared.DTOs
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
        public string PaymentMethod { get; set; }
    }
}

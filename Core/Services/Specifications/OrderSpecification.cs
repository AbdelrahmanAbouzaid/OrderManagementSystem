using Domain.Models;


namespace Services.Specifications
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification()
            : base(null)
        {
            AddInclude(o => o.Customer);
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.Invoice);
        }

        public OrderSpecification(int id)
            : base(o => o.OrderId == id)
        {
            AddInclude(o => o.Customer);
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.Invoice);
        }
       
    }
}

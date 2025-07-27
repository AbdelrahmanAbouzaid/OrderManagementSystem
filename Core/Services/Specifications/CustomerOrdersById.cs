
using Domain.Models;

namespace Services.Specifications
{
    public class OrdersByCustomerIdSpecification : BaseSpecification<Order>
    {
        public OrdersByCustomerIdSpecification(int customerId)
            : base(o => o.CustomerId == customerId)
        {
            AddInclude(o => o.Customer);
        }
        
    }
}

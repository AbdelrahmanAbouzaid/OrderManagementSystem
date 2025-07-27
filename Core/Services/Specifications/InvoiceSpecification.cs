

using Domain.Models;

namespace Services.Specifications
{
    public class InvoiceSpecification : BaseSpecification<Invoice>
    {
        public InvoiceSpecification() : base(null)
        {
            AddInclude(i => i.Order);
        }
        public InvoiceSpecification(int invoiceId) 
            : base(i => i.InvoiceId == invoiceId)
        {
            AddInclude(i => i.Order);
        }
    }
}

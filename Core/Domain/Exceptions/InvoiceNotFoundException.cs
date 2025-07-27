
namespace Domain.Exceptions
{
    public class InvoiceNotFoundException(int invoiceId) : NotFoundException($"Invoice with ID {invoiceId} not found.")
    {
    }
}



using Shared.DTOs;

namespace Services.Abstractions
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync();
        Task<InvoiceDto> GetInvoiceAsync(int invoiceId);
    }
}

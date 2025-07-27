
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetInvoices()
        {
            var invoices = await serviceManager.InvoiceService.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        [HttpGet("{invoiceId:int}")]
        public async Task<IActionResult> GetInvoiceById(int invoiceId)
        {
            var invoice = await serviceManager.InvoiceService.GetInvoiceAsync(invoiceId);
            return Ok(invoice);
        }
    }
}

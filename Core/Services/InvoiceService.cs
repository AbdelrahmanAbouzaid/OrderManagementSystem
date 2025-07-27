
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstractions;
using Services.Specifications;
using Shared.DTOs;

namespace Services
{
    public class InvoiceService(IUnitOfWork unitOfWork, IMapper mapper) : IInvoiceService
    {
        public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync()
        {
            var spec = new InvoiceSpecification();
            var Invoices = await unitOfWork.GetRepository<Invoice>().GetAllAsync(spec);
            return mapper.Map<IEnumerable<InvoiceDto>>(Invoices);   
        }

        public async Task<InvoiceDto> GetInvoiceAsync(int invoiceId)
        {
            var spec = new InvoiceSpecification(invoiceId);
            var Invoice = await unitOfWork.GetRepository<Invoice>().GetByIdAsync(spec);
            if (Invoice == null)
                throw new InvoiceNotFoundException(invoiceId);
            
            return mapper.Map<InvoiceDto>(Invoice);
        }
    }
}

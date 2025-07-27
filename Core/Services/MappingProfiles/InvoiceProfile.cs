

using AutoMapper;
using Domain.Models;
using Shared.DTOs;

namespace Services.MappingProfiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceDto>();

        }
    }
}

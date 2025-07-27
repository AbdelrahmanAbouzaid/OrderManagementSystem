
using AutoMapper;
using Domain.Models;
using Shared.DTOs;

namespace Services.MappingProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();
        }
    }
}

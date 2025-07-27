
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstractions;
using Services.Specifications;
using Shared.DTOs;

namespace Services
{
    public class CustomerService(IUnitOfWork unitOfWork, IMapper mapper) : ICustomerService
    {
        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createDto)
        {
            var customer = mapper.Map<Customer>(createDto);
            await unitOfWork.GetRepository<Customer>().AddAsync(customer);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<CustomerDto>(customer);

        }

        public async Task<IEnumerable<OrderDto>> GetCustomerOrdersAsync(int customerId)
        {
            var spec = new OrdersByCustomerIdSpecification(customerId);
            var orders = await unitOfWork.GetRepository<Order>().GetAllAsync(spec);
            if (orders == null || !orders.Any())
                throw new OrderNotFoundException($"No orders found for customer with ID {customerId}");
            
            return mapper.Map<IEnumerable<OrderDto>>(orders);
        }
    }
}

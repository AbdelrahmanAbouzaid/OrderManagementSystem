using Shared.DTOs;

namespace Services.Abstractions
{
    public interface ICustomerService
    {
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createDto);
        Task<IEnumerable<OrderDto>> GetCustomerOrdersAsync(int customerId);
    }
}

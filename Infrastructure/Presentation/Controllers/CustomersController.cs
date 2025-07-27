
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DTOs;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest("Customer data is required.");
            }
            var result = await serviceManager.CustomerService.CreateCustomerAsync(customerDto);
            
            return Ok(result);
        }

        [HttpGet("{customerId:int}/orders")]
        public async Task<IActionResult> GetCustomerOrders(int customerId)
        {
            var orders = await serviceManager.CustomerService.GetCustomerOrdersAsync(customerId);
            
            return Ok(orders);
        }
    }
}

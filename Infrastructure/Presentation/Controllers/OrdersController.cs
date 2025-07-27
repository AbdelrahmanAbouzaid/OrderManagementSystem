

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DTOs;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            var orders = await serviceManager.OrderService.GetAllOrdersAsync();
            return Ok(orders);
        }
        [HttpGet("{orderId:int}")]
        public async Task<IActionResult> GetOrderByIdAsync(int orderId)
        {
            var order = await serviceManager.OrderService.GetOrderByIdAsync(orderId);
            return Ok(order);
        }

        [HttpPut("{orderId:int}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatusAsync(int orderId, [FromQuery] string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return BadRequest("Status cannot be empty");
            var result = await serviceManager.OrderService.UpdateOrderStatusAsync(orderId, status);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderDto createOrderDto)
        {
            if (createOrderDto == null)
                return BadRequest("Order data cannot be null");

            var order = await serviceManager.OrderService.CreateOrderAsync(createOrderDto);

            return Ok(order);
        }
    }
}

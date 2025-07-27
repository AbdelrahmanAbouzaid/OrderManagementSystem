
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DTOs;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await serviceManager.ProductService.GetProductByIdAsync(productId);
            return Ok(product);
        }

        [HttpPut("{productId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductDto productDto)
        {
            var result = await serviceManager.ProductService.UpdateProductAsync(productId, productDto);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            var result = await serviceManager.ProductService.CreateProductAsync(productDto);
            return Ok(result);
        }
    }
}

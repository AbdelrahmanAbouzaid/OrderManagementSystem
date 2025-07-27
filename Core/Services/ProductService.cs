
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstractions;
using Shared.DTOs;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await unitOfWork.GetRepository<Product>().GetAllAsync();
            return mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            var product = await unitOfWork.GetRepository<Product>().GetByIdAsync(productId);
            if(product is null)
                throw new ProductNotFoundException(productId);
            return mapper.Map<ProductDto>(product);
        }
        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = mapper.Map<Product>(createProductDto);
            await unitOfWork.GetRepository<Product>().AddAsync(product);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<ProductDto>(product);
        }
        public async Task<ProductDto> UpdateProductAsync(int productId, UpdateProductDto updateProductDto)
        {
            var product = await unitOfWork.GetRepository<Product>().GetByIdAsync(productId);
            if (product is null)
                throw new ProductNotFoundException(productId);

            product.Name = updateProductDto.Name;
            product.Price = updateProductDto.Price;
            product.Stock = updateProductDto.Stock;

            unitOfWork.GetRepository<Product>().Update(product);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<ProductDto>(product);
        }
    }
}



using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;

namespace Services
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IMailService mailService) : IServiceManager
    {
        public IOrderService OrderService { get; set; } = new OrderService(unitOfWork, mapper, mailService);
        public ICustomerService CustomerService { get; set; } = new CustomerService(unitOfWork, mapper);
        public IProductService ProductService { get; set; } = new ProductService(unitOfWork, mapper);
    }
}

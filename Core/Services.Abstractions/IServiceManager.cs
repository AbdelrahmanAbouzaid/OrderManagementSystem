
namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IOrderService OrderService { get; }
        ICustomerService CustomerService { get; }
    }
}

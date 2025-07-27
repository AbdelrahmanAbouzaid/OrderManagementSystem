
namespace Domain.Exceptions
{
    public class CustomerNotFoundException(int customerId) : NotFoundException($"Customer with ID {customerId} not found.")
    {
    }
}

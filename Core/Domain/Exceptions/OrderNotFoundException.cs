
namespace Domain.Exceptions
{
    public class OrderNotFoundException(string message = "Orders NotFound") : NotFoundException(message)
    {
    }
}

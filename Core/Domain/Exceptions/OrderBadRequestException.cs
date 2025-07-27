
namespace Domain.Exceptions
{
    public class OrderBadRequestException(string message = "Invalid order") : BadRequestException(message)
    {
    }
}

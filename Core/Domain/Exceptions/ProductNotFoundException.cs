
namespace Domain.Exceptions
{
    public class ProductNotFoundException(int id) :NotFoundException($"Product With id {id} Not Found!")
    {
    }
}

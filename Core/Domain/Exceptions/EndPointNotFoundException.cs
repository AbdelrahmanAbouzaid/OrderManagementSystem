
namespace Domain.Exceptions
{
    public class EndPointNotFoundException (string message) : NotFoundException($"End Piont {message} Not Found!")
    {
    }
}

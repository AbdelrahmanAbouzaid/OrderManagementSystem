

namespace Domain.Exceptions
{
    public class ValidationException(IEnumerable<string> errors) : Exception()
    {
        public IEnumerable<string> Errors { get; } = errors;
    }
}

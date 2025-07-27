using Microsoft.AspNetCore.Http;

namespace Shared.ModelErrors
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; } = StatusCodes.Status404NotFound;
        public string ErrorMessage { get; set; } = "Validation Error!";

        public IEnumerable<ValidationError> Errors { get; set; }
    }
}

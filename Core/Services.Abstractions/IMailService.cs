
using Shared;

namespace Services.Abstractions
{
    public interface IMailService
    {
        bool SendEmail(Email email);
    }
}


using Domain.Models;

namespace Services.Specifications
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(string name)
            : base(u => u.Username == name)
        {
            
        }
    }
}

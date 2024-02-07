using Microsoft.AspNetCore.Identity;

namespace ski_shop.Entities
{
    public class User : IdentityUser<int>
    {
        public UserAddress Address { get; set; }
    }
}
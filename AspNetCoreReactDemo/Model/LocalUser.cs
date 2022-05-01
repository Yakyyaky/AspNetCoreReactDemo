using Microsoft.AspNetCore.Identity;

namespace AspNetCoreReactDemo.Model
{
    public class LocalUser : IdentityUser<string>
    {
    }

    public class UserCredential
    {
        public string Upn { get; set; }
        public string Password { get; set; }
    }
}

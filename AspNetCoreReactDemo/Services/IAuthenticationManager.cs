using AspNetCoreReactDemo.Model;

namespace AspNetCoreReactDemo.Services
{
    public interface IAuthenticationManager
    {
        string Authenticate(UserCredential credential);
    }
}
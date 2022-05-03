using System.Threading.Tasks;
using AspNetCoreReactDemo.Model;

namespace AspNetCoreReactDemo.Services
{
    public interface IAuthenticationManager
    {
        Task<AuthenticatedUser> SignIn(SignInCredential credential);
        Task<bool> SignOut(string upn);
    }
}
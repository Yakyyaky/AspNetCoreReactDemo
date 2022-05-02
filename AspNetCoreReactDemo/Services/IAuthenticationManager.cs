using System.Threading.Tasks;
using AspNetCoreReactDemo.Model;

namespace AspNetCoreReactDemo.Services
{
    public interface IAuthenticationManager
    {
        Task<AuthenticatedUser> SignIn(UserCredential credential);
        Task<bool> LogOut(string upn);
    }
}
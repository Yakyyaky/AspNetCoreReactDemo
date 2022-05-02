using System.Collections;
using System.Threading.Tasks;
using AspNetCoreReactDemo.Model;

namespace AspNetCoreReactDemo.Services
{
    public interface IUserService
    {
        Task<User> GetUser(SignInCredential credential);
        Task<User> CreateUser(User newUser, string password);
    }
}

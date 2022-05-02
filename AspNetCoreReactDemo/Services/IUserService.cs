using System.Collections;
using AspNetCoreReactDemo.Model;

namespace AspNetCoreReactDemo.Services
{
    public interface IUserService
    {
        User GetUser(UserCredential credential);
        User CreateUser(User newUser, string password);
    }
}

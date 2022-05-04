using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreReactDemo.Model;

namespace AspNetCoreReactDemo.Services
{
    public class InMemoryUserService : IUserService
    {
        private class UserData
        {
            public UserData(User user, SignInCredential credential)
            {
                User = user;
                Credential = credential;
            }

            public User User { get; }
            public SignInCredential Credential { get; }
        }

        private readonly IDictionary<string, UserData> _users = new Dictionary<string, UserData>();

        public InMemoryUserService()
        {
            // ReSharper disable StringLiteralTypo
            InternalCreateUser(new User("jordan@gmail.com", "Jordan", "Foo"), @"fphtest");
            InternalCreateUser(new User("test.user@gmail.com", "Test", "User"), @"fphtest");
            // ReSharper restore StringLiteralTypo
        }

        public Task<User> GetUser(SignInCredential credential)
        {
            if (credential.IsValid 
                && _users.TryGetValue(credential.Upn, out var userData) 
                && string.Equals(credential.Password, userData.Credential.Password))
            {
                return Task.FromResult(userData.User);
            }

            return Task.FromResult<User>(null);
        }

        public Task<User> CreateUser(User newUser, string password) => Task.FromResult(InternalCreateUser(newUser, password) ? newUser : null);

        private bool InternalCreateUser(User user, string password)
        {
            if (_users.ContainsKey(user.Upn)) return false;

            _users[user.Upn] = new UserData(user, new SignInCredential(user.Upn, password));
            return true;
        }
    }
}
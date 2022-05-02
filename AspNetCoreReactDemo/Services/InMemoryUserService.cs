using System;
using System.Collections.Generic;
using AspNetCoreReactDemo.Model;

namespace AspNetCoreReactDemo.Services
{
    public class InMemoryUserService : IUserService
    {
        private class UserData
        {
            public UserData(User user, UserCredential credential)
            {
                User = user;
                Credential = credential;
            }

            public User User { get; }
            public UserCredential Credential { get; }
        }

        private readonly IDictionary<string, UserData> _users = new Dictionary<string, UserData>();

        public InMemoryUserService()
        {
            // ReSharper disable StringLiteralTypo
            InternalCreateUser(new User("jpl.foo@gmail.com", "Jordan", "Foo"), @"fphtest");
            InternalCreateUser(new User("test.user@gmail.com", "Test", "User"), @"fphtest");
            // ReSharper restore StringLiteralTypo
        }

        public User GetUser(UserCredential credential)
        {
            if (credential.IsValid 
                && _users.TryGetValue(credential.Upn, out var userData) 
                && string.Equals(credential.Password, userData.Credential.Password))
            {
                return userData.User;
            }

            return null;
        }

        public User CreateUser(User newUser, string password)
        {
            throw new System.NotImplementedException();
        }

        private bool InternalCreateUser(User user, string password)
        {
            if (_users.ContainsKey(user.Upn)) return false;

            _users[user.Upn] = new UserData(user, new UserCredential(user.Upn, password));
            return true;
        }
    }
}
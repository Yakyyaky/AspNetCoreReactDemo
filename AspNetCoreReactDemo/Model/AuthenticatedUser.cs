namespace AspNetCoreReactDemo.Model
{
    public class AuthenticatedUser
    {
        public AuthenticatedUser(User user, string accessToken)
        {
            User = user;
            AccessToken = accessToken;
        }

        public User User { get; }
        public string AccessToken { get; }
    }
}
namespace AspNetCoreReactDemo.Model
{
    public class UserCredential
    {
        // ReSharper disable once UnusedMember.Global
        public UserCredential() { /* for serialization */ }

        public UserCredential(string upn, string password)
        {
            Upn = upn;
            Password = password;
        }

        public string Upn { get; set; }
        public string Password { get; set; }
        public bool IsValid => !string.IsNullOrEmpty(Upn) && !string.IsNullOrEmpty(Password);
    }
}

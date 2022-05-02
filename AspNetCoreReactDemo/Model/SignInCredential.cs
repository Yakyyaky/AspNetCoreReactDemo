namespace AspNetCoreReactDemo.Model
{
    public class SignInCredential
    {
        // ReSharper disable once UnusedMember.Global
        public SignInCredential() { /* for serialization */ }

        public SignInCredential(string upn, string password)
        {
            Upn = upn;
            Password = password;
        }

        public string Upn { get; set; }
        public string Password { get; set; }
        public bool IsValid => !string.IsNullOrEmpty(Upn) && !string.IsNullOrEmpty(Password);
    }
}

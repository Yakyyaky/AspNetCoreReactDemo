namespace AspNetCoreReactDemo.Model
{
    public class User
    {
        // ReSharper disable once UnusedMember.Global
        public User() { /* for serialization */ }
        public User(string upn, string firstName, string lastName)
        {
            Upn = upn;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Upn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
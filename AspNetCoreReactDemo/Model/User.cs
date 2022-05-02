namespace AspNetCoreReactDemo.Model
{
    public class User
    {
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
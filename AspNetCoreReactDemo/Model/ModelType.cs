namespace AspNetCoreReactDemo.Model
{
    /// <summary>
    /// This is the based class use for Authorization filter.
    /// Useful for model that allows users to change authorization behaviour based on 
    /// </summary>
    public class DenyUnlessLoggedInType
    {
        public bool? DenyUnlessLoggedIn { get; set; }
    }

    public class ModelType : DenyUnlessLoggedInType
    {
        public string SomeOtherField { get; set; }
    }

    public class ModelType1
    {
        public bool DenyUnlessLoggedIn { get; set; }
        public string SomeOtherField { get; set; }
    }

    public class ModelType2
    {
        public string SomeOtherField { get; set; }
    }
}
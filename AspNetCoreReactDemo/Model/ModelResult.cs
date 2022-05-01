namespace AspNetCoreReactDemo.Model
{
    public class ModelResult<T>
    {
        public ModelResult(bool success, T data)
        {
            Success = success;
            Data = data;
        }

        public bool Success { get; set; }
        public T Data { get; set; }

    }
}

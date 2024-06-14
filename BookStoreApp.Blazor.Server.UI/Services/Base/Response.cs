namespace BookStoreApp.Blazor.Server.UI.Services.Base
{
    /*
     * This class is used as a template for a response
     */
    public class Response<T>
    {
        public string Message { get; set; }
        public string ValidationErrors { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}

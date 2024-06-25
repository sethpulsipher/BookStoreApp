using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services.Base
{
    public class BaseHttpService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorage;

        public BaseHttpService(IClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException apiException)
        {
            //200 - 299
            if(apiException.StatusCode >=200 &&  apiException.StatusCode <= 299)
            {
                return new Response<Guid>() { Message = "Operation Performed Successfully", Success = true };
            }
            //400
            if (apiException.StatusCode == 400)
            {
                return new Response<Guid>() { Message = "Validation errors have occured.", ValidationErrors = apiException.Response, Success = false };
            }
            //401
            if (apiException.StatusCode == 401)
            {
                return new Response<Guid>() { Message = "Login Failed.", Success = false };
            }
            //404
            if (apiException.StatusCode == 404)
            {
                return new Response<Guid>() { Message = "Something went wrong, please try again.", Success = false };
            }
            //500
            if(apiException.StatusCode == 500)
            {
                return new Response<Guid>() { Message = "500 Server API Issue; ",ValidationErrors = apiException.Response, Success = false };
            }
            // All else..
            return new Response<Guid>() { Message = "Something went wrong, please try again.", Success = false };
        }

        protected async Task GetBearerToken()
        {
            var token = await _localStorage.GetItemAsync<string>("accessToken");
            if(token != null)
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }
    }
}

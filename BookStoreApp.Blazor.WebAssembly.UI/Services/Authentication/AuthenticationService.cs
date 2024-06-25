using Blazored.LocalStorage;
using BookStoreApp.Blazor.WebAssembly.UI.Providers;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services.Authentication
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly IClient _httpClient;
        // accessing broswer storage
        private readonly ILocalStorageService _localStorage;
        // provides information about authentication state of the user
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider) : base(httpClient, localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<Response<AuthResponse>> AuthenticateAsync(LoginUserDto loginModel)
        {
            Response<AuthResponse> response;
            try
            {
                // Get Result of trying to login and set in response
                var result = await _httpClient.LoginAsync(loginModel);
                response = new Response<AuthResponse>
                {
                    Data = result,
                    Success = true
                };

                //Store token provided from login 'Access Name' 'Data to hold'
                await _localStorage.SetItemAsync("accessToken", result.Token);
                //Change auth state of app
                await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<AuthResponse>(ex);
            }
            return response;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }
    }
}

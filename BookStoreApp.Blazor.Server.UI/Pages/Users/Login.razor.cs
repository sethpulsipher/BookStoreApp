using BookStoreApp.Blazor.Server.UI.Services.Authentication;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Blazor.Server.UI.Pages.Users
{
    public partial class Login
    {
        [Inject] IAuthenticationService authService { get; set; }
        [Inject] NavigationManager navManager { get; set; }
        
        LoginUserDto LoginModel = new LoginUserDto();
        string message = string.Empty;
        private bool _loading = false;

        private async Task HandleLogin()
        {
            _loading = true;
            // register the user then navigate to login page
            var response = await authService.AuthenticateAsync(LoginModel);

            if (response.Success)
            {
                navManager.NavigateTo("/");
            }

            _loading = false;
            message = response.Message;
        }
    }
}

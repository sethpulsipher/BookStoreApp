using Microsoft.AspNetCore.Components;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Pages.Users
{
    public partial class Register
    {
        [Inject] IClient httpClient { get; set; }
        [Inject] NavigationManager navManager { get; set; }

        // Defaults a new registration to 'User' role
        UserDto RegistrationModel = new UserDto
        {
            Role = "User"
        };

        string message = string.Empty;


        private async Task HandleRegistration()
        {
            try
            {
                // register the user then navigate to login page 
                await httpClient.RegisterAsync(RegistrationModel);
                navManager.NavigateTo("/users/login");
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode <= 299)
                {
                    navManager.NavigateTo("/users/login");
                }
                message = ex.Response;
            }
        }
    }
}

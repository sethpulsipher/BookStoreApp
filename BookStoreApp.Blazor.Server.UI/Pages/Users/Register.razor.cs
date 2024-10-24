using Microsoft.AspNetCore.Components;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components.Forms;

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
                // register the user 
                await httpClient.RegisterAsync(RegistrationModel);
                navManager.NavigateTo("/users/login");
            }
            catch (ApiException ex)
            {
                // If any status code out of 200
                if (ex.StatusCode >= 200 && ex.StatusCode <= 299)
                {
                    navManager.NavigateTo("/users/login");
                }
                message = ex.Response;
                StateHasChanged();
            }
        }
    }
}

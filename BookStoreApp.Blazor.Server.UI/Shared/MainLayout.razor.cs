using BookStoreApp.Blazor.Server.UI.Services.Authentication;
using BookStoreApp.Blazor.Server.UI.Services.Author;
using Microsoft.AspNetCore.Components;
using System;

namespace BookStoreApp.Blazor.Server.UI.Shared
{
    public partial class MainLayout
    {
        [Inject] IAuthenticationService authService {  get; set; }
        [Inject] NavigationManager navManager { get; set; }

        bool _drawerOpen = true;
        bool _pfpDrawerOpen = false;

        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        protected void ToggleOpenPf() => _pfpDrawerOpen = !_pfpDrawerOpen;

        private async Task Logout()
        {
            // Log user out
            await authService.Logout();
            // Navigate to home
            navManager.NavigateTo("/");
        }
    }
}

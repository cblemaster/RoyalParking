using CommunityToolkit.Mvvm.Input;
using RoyalParking.Core.Services.User;

namespace RoyalParking.MAUI.PageModels;

public partial class LogoutPageModel(IAuthenticationService authService) : PageModelBase(authService)
{
    [RelayCommand]
    private void PageAppearing()
    {
        

    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        HttpAuthenticationService.LogOut();
        await Shell.Current.DisplayAlert("Success!", "You have been logged out and will be redirected to the login page.", "OK");
        await Shell.Current.GoToAsync("///LoginPage");
    }

    [RelayCommand]
    private void Cancel()
    { }
}

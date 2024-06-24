using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RoyalParking.Core.Services.User;
using RoyalParking.MAUI.Messages;

namespace RoyalParking.MAUI.PageModels;

public partial class LogoutPageModel(IAuthenticationService authService) : PageModelBase(authService)
{
    [RelayCommand]
    private async Task LogoutAsync()
    {
        HttpAuthenticationService.LogOut();
        WeakReferenceMessenger.Default.Send(new LoggedInUserChangedMessage(UserService.IsLoggedIn()));
        await Shell.Current.DisplayAlert("Success!", "You have been logged out and will be redirected to the login page.", "OK");
        await Shell.Current.GoToAsync("///LoginPage");
    }
}

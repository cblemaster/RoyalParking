using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RoyalParking.Core.DTO;
using RoyalParking.Core.Interfaces;
using RoyalParking.Core.Services.User;
using RoyalParking.MAUI.Messages;

namespace RoyalParking.MAUI.PageModels;

public partial class LoginPageModel(IAuthenticationService authService) : PageModelBase(authService)
{
    [ObservableProperty]
    private LoginUserDTO loginUser = default!;

    [RelayCommand]
    private void PageAppearing() => LoginUser = new();

    [RelayCommand]
    private async Task LoginAsync()
    {
        IReturnable loginResult = await _authService.LogInAsync(LoginUser);

        if (loginResult is null)
        {
            await Shell.Current.DisplayAlert("Error!", "An unknown error has occured.", "OK");
            return;
        }
        if (loginResult is IResponse)
        {
            // TODO: format the message
            await Shell.Current.DisplayAlert("Error!", $"The following error(s) occured: {(loginResult as IResponse).Message}", "OK");
            return;
        }
        if (loginResult is UserDTO)
        {
            WeakReferenceMessenger.Default.Send(new LoggedInUserChangedMessage(UserService.IsLoggedIn()));
            await Shell.Current.DisplayAlert("Success!", "You have been logged in.", "OK");
            await Shell.Current.GoToAsync("///AboutPage");
            return;
        }
    }

    [RelayCommand]
    private void Cancel() => PageAppearing();
}

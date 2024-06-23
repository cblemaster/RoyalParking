using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoyalParking.Core.DTO;
using RoyalParking.Core.Interfaces;
using RoyalParking.Core.Services.User;
using System.Collections.ObjectModel;

namespace RoyalParking.MAUI.PageModels
{
    public partial class RegisterPageModel(IAuthenticationService authService) : PageModelBase(authService)
    {
        [ObservableProperty]
        private RegisterUserDTO createUser = default!;

        [ObservableProperty]
        private ObservableCollection<string> roles = default!;

        [ObservableProperty]
        private string selectedRole = default!;

        [RelayCommand]
        private void PageAppearing()
        {
            CreateUser = new();
            Roles = ["Customer", "Valet"];
            SelectedRole = null!;
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            CreateUser.Role = SelectedRole;
            IReturnable registerResult = await _authService.RegisterAsync(CreateUser);
            if (registerResult is null)
            {
                await Shell.Current.DisplayAlert("Error!", "An unknown error has occured.", "OK");
                return;
            }
            if (registerResult is IResponse)
            {
                // TODO: format the message
                await Shell.Current.DisplayAlert("Error!", $"The following error(s) occured: {(registerResult as IResponse).Message}", "OK");
                return;
            }
            if (registerResult is UserDTO)
            {
                await Shell.Current.DisplayAlert("Success!", "You have been registered as a new user and will be redirected to the login page.", "OK");
                await Shell.Current.GoToAsync("///LoginPage");
            }
        }

        [RelayCommand]
        private void Cancel() => PageAppearing();
    }
}

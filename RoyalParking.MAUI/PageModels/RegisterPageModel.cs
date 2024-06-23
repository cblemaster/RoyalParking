using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoyalParking.Core.DTO;
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
            Roles = new() { "Customer", "Valet" };
            SelectedRole = null!;
        }
        
        [RelayCommand]
        private void Register()
        {
            CreateUser.Role = SelectedRole;
            _authService.Register(CreateUser);
        }

        [RelayCommand]
        private void Cancel()
        { }
    }
}

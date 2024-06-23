using CommunityToolkit.Mvvm.ComponentModel;
using RoyalParking.Core.Services.User;

namespace RoyalParking.MAUI.PageModels;

public abstract class PageModelBase(IAuthenticationService authService) : ObservableObject
{
    internal readonly IAuthenticationService _authService = authService;
}

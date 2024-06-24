using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using RoyalParking.MAUI.Messages;

namespace RoyalParking.MAUI.PageModels;

public partial class AppShellModel : ObservableObject
{
    [ObservableProperty]
    private bool isUserLoggedIn;
    public AppShellModel() =>
       WeakReferenceMessenger.Default.Register<LoggedInUserChangedMessage>(this, (r, m) =>
           IsUserLoggedIn = m.Value);
}

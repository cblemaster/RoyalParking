using RoyalParking.MAUI.PageModels;

namespace RoyalParking.MAUI.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}
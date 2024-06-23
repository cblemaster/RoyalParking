using RoyalParking.MAUI.PageModels;

namespace RoyalParking.MAUI.Pages;

public partial class LogoutPage : ContentPage
{
    public LogoutPage(LogoutPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}
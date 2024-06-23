using RoyalParking.MAUI.PageModels;

namespace RoyalParking.MAUI.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageModel pageModel)
	{
		InitializeComponent();
		BindingContext = pageModel;
	}
}
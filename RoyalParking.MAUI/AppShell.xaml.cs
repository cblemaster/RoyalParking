using RoyalParking.MAUI.PageModels;

namespace RoyalParking.MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellModel shellModel)
        {
            InitializeComponent();
            BindingContext = shellModel;
        }
    }
}

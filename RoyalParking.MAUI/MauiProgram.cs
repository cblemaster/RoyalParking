using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using RoyalParking.Core.Services.User;
using RoyalParking.MAUI.PageModels;
using RoyalParking.MAUI.Pages;

namespace RoyalParking.MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .Services.AddSingleton<AppShell>()
                     .AddSingleton<IAuthenticationService, HttpAuthenticationService>()
                     .AddTransient<RegisterPageModel>()
                     .AddTransient<RegisterPage>()
                     .AddTransient<LoginPageModel>()
                     .AddTransient<LoginPage>()
                     .AddTransient<LogoutPageModel>()
                     .AddTransient<LogoutPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

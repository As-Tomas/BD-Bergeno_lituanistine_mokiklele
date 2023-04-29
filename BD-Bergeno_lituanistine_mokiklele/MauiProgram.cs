using BD_Bergeno_lituanistine_mokiklele.Services;
using BD_Bergeno_lituanistine_mokiklele.ViewModels;
using BD_Bergeno_lituanistine_mokiklele.ViewModels.Dashboard;
using BD_Bergeno_lituanistine_mokiklele.ViewModels.Startup;
using BD_Bergeno_lituanistine_mokiklele.Views.Dashboard;
using BD_Bergeno_lituanistine_mokiklele.Views.Startup;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace BD_Bergeno_lituanistine_mokiklele;

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
			});

        builder.Services.AddSingleton<ILoginService, LoginService>();
        builder.Services.AddSingleton<IRegisterService, RegisterService>();

        //Views
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<DashboardPage>();
        builder.Services.AddSingleton<LoadingPage>();
        builder.Services.AddSingleton<RegisterPage>();

        builder.Services.AddSingleton<ProfileDashboardPage>();



        //View Models
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<DashboardPageViewModel>();
        builder.Services.AddSingleton<LoadingPageViewModel>();
        builder.Services.AddSingleton<RegisterPageViewModel>();

        builder.Services.AddSingleton<ProfileDashboardPageViewModel>();

        return builder.Build();
    }
}

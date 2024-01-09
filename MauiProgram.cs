using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Bislerium.Data;
using MudBlazor;

namespace Bislerium
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.VisibleStateDuration = 1000;
                config.SnackbarConfiguration.HideTransitionDuration = 200;
                config.SnackbarConfiguration.ShowTransitionDuration = 200;
                config.SnackbarConfiguration.MaxDisplayedSnackbars = 6;
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomStart;
            });

            builder.Services.AddSingleton<CoffeeServices>();
            builder.Services.AddSingleton<AddinsServices>();
            builder.Services.AddSingleton<UserServices>();
            builder.Services.AddSingleton<OrderItemsServices>();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

using Microsoft.Extensions.Logging;
using boadaDP3.Services;
using boadaDP3.ViewModels;
using boadaDP3.Views;

namespace boadaDP3;

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

#if DEBUG
        builder.Services.AddLogging(logging =>
        {
            logging.AddDebug();
        });
#endif

        // Servicios
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<LogService>();

        // ViewModels
        builder.Services.AddTransient<NuevoEquipoViewModel>();
        builder.Services.AddTransient<ListaEquiposViewModel>();
        builder.Services.AddTransient<LogsViewModel>();

        // Views
        builder.Services.AddTransient<NuevoEquipoPage>();
        builder.Services.AddTransient<ListaEquiposPage>();
        builder.Services.AddTransient<LogsPage>();

        return builder.Build();
    }
}
using Microsoft.Extensions.Logging;
using MyTrainer.Data;


namespace MyTrainer;

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
        builder.Services.AddDbContext<TrainingAppContext>()
            .AddScoped(typeof(Repository<>));
        builder.Services.AddBlazorBootstrap();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        
        return builder.Build();
    }
    
}

using BetterBillableHours.Data;
using Microsoft.Extensions.Logging;

namespace BetterBillableHours;

public static class MauiProgram
{
    public static HoursDatabase Database = new();
    public static MauiApp CreateMauiApp()
    {
        Database.Database.EnsureCreated();
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
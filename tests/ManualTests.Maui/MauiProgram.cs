using ManualTests.Maui.Services;
using ManualTests.Tests.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Plugin.Toast;

namespace ManualTests.Maui
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
            builder.Services
                .AddNotificationManager()
                .AddNotificationManagerImagesSupport(() => "")
                .AddLogging(_ => _.AddDebug())
                .AddTests()
                .AddTransient<MainPage>()
                .AddSingleton<ICoreDispatcher, CoreDispatcher>()
                .AddSingleton<IRuntimePlatform, RuntimePlatform>()
                .AddSingleton<ICommandFactory, CommandFactory>()
                .AddSingleton<ICommandUpdater, CommandUpdater>();
            return builder.Build();
        }
    }
}
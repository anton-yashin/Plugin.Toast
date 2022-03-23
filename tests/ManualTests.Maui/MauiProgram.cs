using ManualTests.Maui.Services;
using ManualTests.Tests.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Platform;
#if WINDOWS
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
#endif
using Microsoft.Maui.Hosting;
using Plugin.Toast;
using System;
using System.Linq;

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
                .AddNotificationManager(
#if __ANDROID__
                b => b.WithActivity(sp => GetActivity(sp))
#endif
                )
                .AddNotificationManagerImagesSupport(
#if WINDOWS
                GetImageDirectory
#endif
                )
                .AddLogging(_ => _.AddDebug())
                .AddTests()
                .AddTransient<MainPage>()
                .AddSingleton<ICoreDispatcher, CoreDispatcher>()
                .AddSingleton<IRuntimePlatform, RuntimePlatform>()
                .AddSingleton<ICommandFactory, CommandFactory>()
                .AddSingleton<ICommandUpdater, CommandUpdater>();
            return builder.Build();
        }

#if __ANDROID__
        static global::Android.App.Activity GetActivity(IServiceProvider sp)
            => (from i in sp.GetRequiredService<IApplication>().Windows
                let mw = i as Microsoft.Maui.Controls.Window
                where mw != null
                let nw = mw.Handler.MauiContext?.Services.GetService<global::Android.App.Activity>()
                where nw != null
                select nw).FirstOrDefault()
            ?? throw new InvalidOperationException("activity not found");

#endif

#if WINDOWS
        static string GetImageDirectory()
            => Microsoft.Maui.Controls.Application.Current.OnThisPlatform().GetImageDirectory();
#endif
    }
}
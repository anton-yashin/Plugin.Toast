using ManualTests.Maui.Services;
using ManualTests.Tests.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Plugin.Toast;
using System;

#if __ANDROID__
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

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
#if __ANDROID__
                (sp, fn) => GetActivity(sp).Resources.GetBitmapAsync(fn)
#elif __IOS__
#else
                () => ""
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
            => (global::Android.App.Activity)(sp.GetRequiredService<IMauiContext>().Context
                ?? throw new InvalidOperationException("activity not found"));

#endif
    }
}
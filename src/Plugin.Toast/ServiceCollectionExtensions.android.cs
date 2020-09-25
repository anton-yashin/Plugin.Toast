using Android.App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Plugin.Toast.Droid;

namespace Plugin.Toast
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, Activity activity)
            => @this.AddNotificationManager(new ToastOptions(activity));

        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, IToastOptions options)
        {
            _ = @this ?? throw new ArgumentNullException(nameof(@this));
            _ = options ?? throw new ArgumentNullException(nameof(@this));
            @this.TryAddSingleton<IToastOptions>(_ => options);
            @this.TryAddSingleton<IIntentManager, IntentManager>();
            @this.TryAddTransient(typeof(IBuilder), options.NotificationStyle.Resolve(typeof(SnackbarBuilder), typeof(NotificationBuilder)));
            @this.TryAddTransient<ISnackbarExtension, SnackbarBuilder>();
            @this.TryAddTransient<IDroidNotificationExtension, NotificationBuilder>();
            return @this.AddBase();
        }

        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, IToastOptions options,
            Action<IPlatformSpecificExtension> defaultPlatformConfiguration,
            Action<ISnackbarExtension> defaultSnackbarConfiguration)
        {
            _ = defaultPlatformConfiguration ?? throw new ArgumentNullException(nameof(defaultPlatformConfiguration));
            _ = defaultSnackbarConfiguration ?? throw new ArgumentNullException(nameof(defaultSnackbarConfiguration));
            return @this.AddNotificationManager(options)
                .AddSingleton<IExtensionConfiguration<IPlatformSpecificExtension>>(
                sp => new DefaultConfiguration<IPlatformSpecificExtension>(defaultPlatformConfiguration))
                .AddSingleton<IExtensionConfiguration<ISnackbarExtension>>(
                sp => new DefaultConfiguration<ISnackbarExtension>(defaultSnackbarConfiguration));
        }

        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, Activity activity,
            Action<IPlatformSpecificExtension> defaultPlatformConfiguration,
            Action<ISnackbarExtension> defaultSnackbarConfiguration)
        {
            return @this.AddNotificationManager(new ToastOptions(activity), defaultPlatformConfiguration, defaultSnackbarConfiguration);
        }

        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, IToastOptions options,
            Action<IPlatformSpecificExtension> defaultPlatformConfiguration)
        {
            _ = defaultPlatformConfiguration ?? throw new ArgumentNullException(nameof(defaultPlatformConfiguration));
            return @this.AddNotificationManager(options)
                .AddSingleton<IExtensionConfiguration<IPlatformSpecificExtension>>(
                sp => new DefaultConfiguration<IPlatformSpecificExtension>(defaultPlatformConfiguration));
        }

        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, Activity activity,
            Action<IPlatformSpecificExtension> defaultPlatformConfiguration)
        {
            return @this.AddNotificationManager(new ToastOptions(activity), defaultPlatformConfiguration);
        }

        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, IToastOptions options,
            Action<ISnackbarExtension> defaultSnackbarConfiguration)
        {
            _ = defaultSnackbarConfiguration ?? throw new ArgumentNullException(nameof(defaultSnackbarConfiguration));
            return @this.AddNotificationManager(options)
                .AddSingleton<IExtensionConfiguration<ISnackbarExtension>>(
                sp => new DefaultConfiguration<ISnackbarExtension>(defaultSnackbarConfiguration));
        }

        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, Activity activity,
            Action<ISnackbarExtension> defaultSnackbarConfiguration)
        {
            return @this.AddNotificationManager(new ToastOptions(activity), defaultSnackbarConfiguration);
        }
    }
}

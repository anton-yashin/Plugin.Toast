using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Plugin.Toast.UWP;

namespace Plugin.Toast
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this)
            => AddNotificationManager(@this, new ToastOptions());

        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, IToastOptions options)
        {
            _ = @this ?? throw new ArgumentNullException(nameof(@this));
            _ = options ?? throw new ArgumentNullException(nameof(options));
            @this.TryAddTransient<IBuilder, NotificationBuilder>();
            @this.TryAddSingleton<IHistory, History>();
            @this.TryAddSingleton<IActivator, UwpActivator>();
            return @this.AddBase();
        }

        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, 
            IToastOptions options,
            Action<IPlatformSpecificExtension> defaultConfiguration)
        {
            _ = defaultConfiguration ?? throw new ArgumentNullException(nameof(defaultConfiguration));
            return @this.AddNotificationManager(options)
                .AddSingleton<IExtensionConfiguration<IPlatformSpecificExtension>>(
                sp => new DefaultConfiguration<IPlatformSpecificExtension>(defaultConfiguration));
        }

        public static IServiceCollection AddNotificationManager(this IServiceCollection @this,
            Action<IPlatformSpecificExtension> defaultConfiguration)
        {
            _ = defaultConfiguration ?? throw new ArgumentNullException(nameof(defaultConfiguration));
            return @this.AddNotificationManager(new ToastOptions())
                .AddSingleton<IExtensionConfiguration<IPlatformSpecificExtension>>(
                sp => new DefaultConfiguration<IPlatformSpecificExtension>(defaultConfiguration));
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Plugin.Toast.UWP;
using Plugin.Toast.Abstractions;

namespace Plugin.Toast
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the notification manager and other services to the service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IToastOptions"/>,<br/>
        /// <seealso cref="INotificationManager"/>,<br/>
        /// <seealso cref="IHistory"/>,<br/>
        /// <seealso cref="INotificationEventSource"/>,<br/>
        /// <seealso cref="ISystemEventSource"/>,<br/>
        /// <seealso cref="IExtensionConfiguration{T}"/><br/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this)
            => AddNotificationManager(@this, new ToastOptions());

        /// <summary>
        /// Add the notification manager and other services to the service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IToastOptions"/>,<br/>
        /// <seealso cref="INotificationManager"/>,<br/>
        /// <seealso cref="IHistory"/>,<br/>
        /// <seealso cref="INotificationEventSource"/>,<br/>
        /// <seealso cref="ISystemEventSource"/>,<br/>
        /// <seealso cref="IExtensionConfiguration{T}"/><br/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="options">Additional options</param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, IToastOptions options)
        {
            _ = @this ?? throw new ArgumentNullException(nameof(@this));
            _ = options ?? throw new ArgumentNullException(nameof(options));
            @this.TryAddTransient<INotificationBuilder, NotificationBuilder>();
            @this.TryAddSingleton<IHistory, History>();
            return @this.AddBase();
        }

        /// <summary>
        /// Add the notification manager and other services to the service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IToastOptions"/>,<br/>
        /// <seealso cref="INotificationManager"/>,<br/>
        /// <seealso cref="IHistory"/>,<br/>
        /// <seealso cref="INotificationEventSource"/>,<br/>
        /// <seealso cref="ISystemEventSource"/>,<br/>
        /// <seealso cref="IExtensionConfiguration{T}"/><br/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="options">Additional options</param>
        /// <param name="defaultConfiguration">Default configuration for <see cref="IPlatformSpecificExtension"/></param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, 
            IToastOptions options,
            Action<IPlatformSpecificExtension> defaultConfiguration)
        {
            _ = defaultConfiguration ?? throw new ArgumentNullException(nameof(defaultConfiguration));
            return @this.AddNotificationManager(options)
                .AddSingleton<IExtensionConfiguration<IPlatformSpecificExtension>>(
                sp => new DefaultConfiguration<IPlatformSpecificExtension>(defaultConfiguration));
        }

        /// <summary>
        /// Add the notification manager and other services to the service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IToastOptions"/>,<br/>
        /// <seealso cref="INotificationManager"/>,<br/>
        /// <seealso cref="IHistory"/>,<br/>
        /// <seealso cref="INotificationEventSource"/>,<br/>
        /// <seealso cref="ISystemEventSource"/>,<br/>
        /// <seealso cref="IExtensionConfiguration{T}"/><br/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="defaultConfiguration">Default configuration for <see cref="IPlatformSpecificExtension"/></param>
        /// <returns>Service collection from @this parameter</returns>
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

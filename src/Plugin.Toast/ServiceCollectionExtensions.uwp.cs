using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Plugin.Toast.UWP;
using Plugin.Toast.Abstractions;

namespace Plugin.Toast
{
    /// <summary>
    /// UWP extensions for <see cref="IServiceCollection"/>
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the notification manager and other services to the service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="INotificationManager"/>,<br/>
        /// <seealso cref="IHistory"/>,<br/>
        /// <seealso cref="INotificationEventSource"/>,<br/>
        /// <seealso cref="ISystemEventSource"/>,<br/>
        /// <seealso cref="IExtensionConfiguration{T}"/><br/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this)
        {
            _ = @this ?? throw new ArgumentNullException(nameof(@this));
            @this.TryAddTransient<INotificationBuilder, NotificationBuilder>();
            @this.TryAddSingleton<IHistory, History>();
            return @this.AddBase();
        }

        /// <summary>
        /// Add the notification manager and other services to the service collection. 
        /// Services to be added to the collection:
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
            return @this.AddNotificationManager()
                .AddSingleton<IExtensionConfiguration<IPlatformSpecificExtension>>(
                sp => new DefaultConfiguration<IPlatformSpecificExtension>(defaultConfiguration));
        }
    }
}

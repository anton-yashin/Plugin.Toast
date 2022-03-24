using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using UIKit;
using Plugin.Toast.IOS;
using Plugin.Toast.Abstractions;

namespace Plugin.Toast
{
    /// <summary>
    /// iOS extensions for <see cref="IServiceCollection"/>
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
        /// <seealso cref="INotificationReceiver"/>,<br/>
        /// <seealso cref="IPermission"/>,<br/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this)
        {
            _ = @this ?? throw new ArgumentNullException(nameof(@this));
            @this.TryAddSingleton<INotificationReceiver, NotificationReceiver>();
            @this.TryAddTransient(typeof(INotificationBuilder), UIDevice.CurrentDevice.CheckSystemVersion(10, 0)
                ? typeof(NotificationBuilder) : typeof(LocalNotificationBuilder));
            @this.TryAddTransient<IIosNotificationExtension, NotificationBuilder>();
#pragma warning disable CA1416 // Validate platform compatibility
            @this.TryAddTransient<IIosLocalNotificationExtension, LocalNotificationBuilder>();
#pragma warning restore CA1416 // Validate platform compatibility
            @this.TryAddSingleton<IPermission, Permission>();
            @this.TryAddSingleton(typeof(IInitialization), _ => _.GetRequiredService(typeof(IPermission)));
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
        /// <seealso cref="INotificationReceiver"/>,<br/>
        /// <seealso cref="IPermission"/>,<br/>
        /// <seealso cref="IExtensionConfiguration{T}"/><br/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="defaultConfiguration">Default configuration for <see cref="IPlatformSpecificExtension"/></param>
        /// <param name="localNotificationConiguration">Default configuration for <see cref="IIosLocalNotificationExtension"/></param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this,
            Action<IPlatformSpecificExtension> defaultConfiguration,
            Action<IIosLocalNotificationExtension> localNotificationConiguration)
        {
            _ = defaultConfiguration ?? throw new ArgumentNullException(nameof(defaultConfiguration));
            _ = localNotificationConiguration ?? throw new ArgumentNullException(nameof(localNotificationConiguration));
#pragma warning disable CA1416 // Validate platform compatibility
            return @this.AddNotificationManager()
                .AddSingleton<IExtensionConfiguration<IPlatformSpecificExtension>>(
                sp => new DefaultConfiguration<IPlatformSpecificExtension>(defaultConfiguration))
                .AddSingleton<IExtensionConfiguration<IIosLocalNotificationExtension>>(
                sp => new DefaultConfiguration<IIosLocalNotificationExtension>(localNotificationConiguration));
#pragma warning restore CA1416 // Validate platform compatibility
        }

        /// <summary>
        /// Add the notification manager and other services to the service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="INotificationManager"/>,<br/>
        /// <seealso cref="IHistory"/>,<br/>
        /// <seealso cref="INotificationEventSource"/>,<br/>
        /// <seealso cref="ISystemEventSource"/>,<br/>
        /// <seealso cref="INotificationReceiver"/>,<br/>
        /// <seealso cref="IPermission"/>,<br/>
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

        /// <summary>
        /// Add the notification manager and other services to the service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="INotificationManager"/>,<br/>
        /// <seealso cref="IHistory"/>,<br/>
        /// <seealso cref="INotificationEventSource"/>,<br/>
        /// <seealso cref="ISystemEventSource"/>,<br/>
        /// <seealso cref="INotificationReceiver"/>,<br/>
        /// <seealso cref="IPermission"/>,<br/>
        /// <seealso cref="IExtensionConfiguration{T}"/><br/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="localNotificationConiguration">Default configuration for <see cref="IIosLocalNotificationExtension"/></param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this,
            Action<IIosLocalNotificationExtension> localNotificationConiguration)
        {
            _ = localNotificationConiguration ?? throw new ArgumentNullException(nameof(localNotificationConiguration));
#pragma warning disable CA1416 // Validate platform compatibility
            return @this.AddNotificationManager()
                .AddSingleton<IExtensionConfiguration<IIosLocalNotificationExtension>>(
                sp => new DefaultConfiguration<IIosLocalNotificationExtension>(localNotificationConiguration));
#pragma warning restore CA1416 // Validate platform compatibility
        }
    }
}

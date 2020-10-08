using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using UIKit;
using Plugin.Toast.IOS;

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
        /// <seealso cref="INotificationReceiver"/>,<br/>
        /// <seealso cref="IPermission"/>,<br/>
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
        /// <seealso cref="INotificationReceiver"/>,<br/>
        /// <seealso cref="IPermission"/>,<br/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="options">Additional options</param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, IToastOptions options)
        {
            _ = @this ?? throw new ArgumentNullException(nameof(@this));
            _ = options ?? throw new ArgumentNullException(nameof(options));
            @this.TryAddSingleton<IToastOptions>(_ => options);
            @this.TryAddSingleton<INotificationReceiver, NotificationReceiver>();
            @this.TryAddTransient(typeof(IBuilder), UIDevice.CurrentDevice.CheckSystemVersion(10, 0)
                ? typeof(NotificationBuilder) : typeof(LocalNotificationBuilder));
            @this.TryAddTransient<IIosNotificationExtension, NotificationBuilder>();
            @this.TryAddTransient<IIosLocalNotificationExtension, LocalNotificationBuilder>();
            @this.TryAddSingleton<IPermission, Permission>();
            @this.TryAddSingleton(typeof(IInitialization), _ => _.GetService(typeof(IPermission)));
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
        /// <seealso cref="INotificationReceiver"/>,<br/>
        /// <seealso cref="IPermission"/>,<br/>
        /// <seealso cref="IExtensionConfiguration{T}"/><br/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="options">Additional options</param>
        /// <param name="defaultConfiguration">Default configuration for <see cref="IPlatformSpecificExtension"/></param>
        /// <param name="localNotificationConiguration">Default configuration for <see cref="IIosLocalNotificationExtension"/></param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this,
            IToastOptions options, 
            Action<IPlatformSpecificExtension> defaultConfiguration,
            Action<IIosLocalNotificationExtension> localNotificationConiguration)
        {
            _ = defaultConfiguration ?? throw new ArgumentNullException(nameof(defaultConfiguration));
            _ = localNotificationConiguration ?? throw new ArgumentNullException(nameof(localNotificationConiguration));
            return @this.AddNotificationManager(options)
                .AddSingleton<IExtensionConfiguration<IPlatformSpecificExtension>>(
                sp => new DefaultConfiguration<IPlatformSpecificExtension>(defaultConfiguration))
                .AddSingleton<IExtensionConfiguration<IIosLocalNotificationExtension>>(
                sp => new DefaultConfiguration<IIosLocalNotificationExtension>(localNotificationConiguration));
        }

        /// <summary>
        /// Add the notification manager and other services to the service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IToastOptions"/>,<br/>
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
            return @this.AddNotificationManager(new ToastOptions())
                .AddSingleton<IExtensionConfiguration<IPlatformSpecificExtension>>(
                sp => new DefaultConfiguration<IPlatformSpecificExtension>(defaultConfiguration))
                .AddSingleton<IExtensionConfiguration<IIosLocalNotificationExtension>>(
                sp => new DefaultConfiguration<IIosLocalNotificationExtension>(localNotificationConiguration));
        }

        /// <summary>
        /// Add the notification manager and other services to the service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IToastOptions"/>,<br/>
        /// <seealso cref="INotificationManager"/>,<br/>
        /// <seealso cref="IHistory"/>,<br/>
        /// <seealso cref="INotificationEventSource"/>,<br/>
        /// <seealso cref="ISystemEventSource"/>,<br/>
        /// <seealso cref="INotificationReceiver"/>,<br/>
        /// <seealso cref="IPermission"/>,<br/>
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
            return @this.AddNotificationManager(new ToastOptions())
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
        /// <seealso cref="INotificationReceiver"/>,<br/>
        /// <seealso cref="IPermission"/>,<br/>
        /// <seealso cref="IExtensionConfiguration{T}"/><br/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="options">Additional options</param>
        /// <param name="localNotificationConiguration">Default configuration for <see cref="IIosLocalNotificationExtension"/></param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this,
            IToastOptions options,
            Action<IIosLocalNotificationExtension> localNotificationConiguration)
        {
            _ = localNotificationConiguration ?? throw new ArgumentNullException(nameof(localNotificationConiguration));
            return @this.AddNotificationManager(options)
                .AddSingleton<IExtensionConfiguration<IIosLocalNotificationExtension>>(
                sp => new DefaultConfiguration<IIosLocalNotificationExtension>(localNotificationConiguration));
        }

        /// <summary>
        /// Add the notification manager and other services to the service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IToastOptions"/>,<br/>
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
            return @this.AddNotificationManager(new ToastOptions())
                .AddSingleton<IExtensionConfiguration<IIosLocalNotificationExtension>>(
                sp => new DefaultConfiguration<IIosLocalNotificationExtension>(localNotificationConiguration));
        }
    }
}

using Android.App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Plugin.Toast.Droid;

namespace Plugin.Toast
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the notification manager and several other services to service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IInitialization"/> with dummy implementation,
        /// <seealso cref="INotificationManager"/>, <seealso cref="IIntentManager"/>
        /// <seealso cref="IToastOptions"/> with default values
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="activity">Activity that used to show a snackbar notification.</param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, Activity activity)
            => @this.AddNotificationManager(new ToastOptions(activity));

        /// <summary>
        /// Add the notification manager and several other services to service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IInitialization"/> with dummy implementation,
        /// <seealso cref="INotificationManager"/>, <seealso cref="IIntentManager"/>,
        /// <seealso cref="IToastOptions"/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="options">Additional options</param>
        /// <returns>Service collection from @this parameter</returns>
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

        /// <summary>
        /// Add the notification manager and several other services to service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IInitialization"/> with dummy implementation,
        /// <seealso cref="INotificationManager"/>, <seealso cref="IIntentManager"/>,
        /// <seealso cref="IToastOptions"/>, <seealso cref="IExtensionConfiguration{T}"/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="options">Additional options</param>
        /// <param name="defaultPlatformConfiguration">Default configuration for <see cref="IPlatformSpecificExtension"/></param>
        /// <param name="defaultSnackbarConfiguration">Default configuration for <see cref="ISnackbarExtension"/></param>
        /// <returns>Service collection from @this parameter</returns>
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

        /// <summary>
        /// Add the notification manager and several other services to service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IInitialization"/> with dummy implementation,
        /// <seealso cref="INotificationManager"/>, <seealso cref="IIntentManager"/>,
        /// <seealso cref="IToastOptions"/>, <seealso cref="IExtensionConfiguration{T}"/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="activity">Activity that used to show a snackbar notification.</param>
        /// <param name="defaultPlatformConfiguration">Default configuration for <see cref="IPlatformSpecificExtension"/></param>
        /// <param name="defaultSnackbarConfiguration">Default configuration for <see cref="ISnackbarExtension"/></param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, Activity activity,
            Action<IPlatformSpecificExtension> defaultPlatformConfiguration,
            Action<ISnackbarExtension> defaultSnackbarConfiguration)
        {
            return @this.AddNotificationManager(new ToastOptions(activity), defaultPlatformConfiguration, defaultSnackbarConfiguration);
        }

        /// <summary>
        /// Add the notification manager and several other services to service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IInitialization"/> with dummy implementation,
        /// <seealso cref="INotificationManager"/>, <seealso cref="IIntentManager"/>,
        /// <seealso cref="IToastOptions"/>, <seealso cref="IExtensionConfiguration{T}"/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="options">Additional options</param>
        /// <param name="defaultPlatformConfiguration">Default configuration for <see cref="IPlatformSpecificExtension"/></param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, IToastOptions options,
            Action<IPlatformSpecificExtension> defaultPlatformConfiguration)
        {
            _ = defaultPlatformConfiguration ?? throw new ArgumentNullException(nameof(defaultPlatformConfiguration));
            return @this.AddNotificationManager(options)
                .AddSingleton<IExtensionConfiguration<IPlatformSpecificExtension>>(
                sp => new DefaultConfiguration<IPlatformSpecificExtension>(defaultPlatformConfiguration));
        }

        /// <summary>
        /// Add the notification manager and several other services to service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IInitialization"/> with dummy implementation,
        /// <seealso cref="INotificationManager"/>, <seealso cref="IIntentManager"/>,
        /// <seealso cref="IToastOptions"/>, <seealso cref="IExtensionConfiguration{T}"/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="activity">Activity that used to show a snackbar notification.</param>
        /// <param name="defaultPlatformConfiguration">Default configuration for <see cref="IPlatformSpecificExtension"/></param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, Activity activity,
            Action<IPlatformSpecificExtension> defaultPlatformConfiguration)
        {
            return @this.AddNotificationManager(new ToastOptions(activity), defaultPlatformConfiguration);
        }

        /// <summary>
        /// Add the notification manager and several other services to service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IInitialization"/> with dummy implementation,
        /// <seealso cref="INotificationManager"/>, <seealso cref="IIntentManager"/>,
        /// <seealso cref="IToastOptions"/>, <seealso cref="IExtensionConfiguration{T}"/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="options">Additional options</param>
        /// <param name="defaultSnackbarConfiguration">Default configuration for <see cref="ISnackbarExtension"/></param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, IToastOptions options,
            Action<ISnackbarExtension> defaultSnackbarConfiguration)
        {
            _ = defaultSnackbarConfiguration ?? throw new ArgumentNullException(nameof(defaultSnackbarConfiguration));
            return @this.AddNotificationManager(options)
                .AddSingleton<IExtensionConfiguration<ISnackbarExtension>>(
                sp => new DefaultConfiguration<ISnackbarExtension>(defaultSnackbarConfiguration));
        }

        /// <summary>
        /// Add the notification manager and several other services to service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IInitialization"/> with dummy implementation,
        /// <seealso cref="INotificationManager"/>, <seealso cref="IIntentManager"/>,
        /// <seealso cref="IToastOptions"/>, <seealso cref="IExtensionConfiguration{T}"/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="activity">Activity that used to show a snackbar notification.</param>
        /// <param name="defaultSnackbarConfiguration">Default configuration for <see cref="ISnackbarExtension"/></param>
        /// <returns>Service collection from @this parameter</returns>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, Activity activity,
            Action<ISnackbarExtension> defaultSnackbarConfiguration)
        {
            return @this.AddNotificationManager(new ToastOptions(activity), defaultSnackbarConfiguration);
        }
    }
}

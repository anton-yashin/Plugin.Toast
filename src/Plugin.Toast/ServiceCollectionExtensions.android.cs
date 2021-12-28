using Android.App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Plugin.Toast.Droid;
using Plugin.Toast.Abstractions;
using System.Linq;
using Plugin.Toast.Droid.Configuration;

namespace Plugin.Toast
{
    /// <summary>
    /// Android extensions for <see cref="IServiceCollection"/>
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add the notification manager and other services to the service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IInitialization"/> with dummy implementation,<br/>
        /// <seealso cref="INotificationManager"/>,<br/>
        /// <seealso cref="IIntentManager"/>,<br/>
        /// <seealso cref="IAndroidNotificationManager"/>,<br/>
        /// <seealso cref="ISystemEventSource"/>,<br/>
        /// <seealso cref="INotificationEventSource"/>,<br/>
        /// <seealso cref="IHistory"/><br/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="configureAction">The delegate for configuring <see cref="IConfigurationBuilder"/>
        /// that provides additional information for notification manager</param>
        /// <returns>Service collection from @this parameter</returns>
        /// <exception cref="InvalidOperationException">When
        /// <see cref="ConfigurationBuilderExtensions.WithActivity(IConfigurationBuilder, Activity)"/> or
        /// <see cref="ConfigurationBuilderExtensions.WithActivity(IConfigurationBuilder, Func{IServiceProvider, Activity})"/>
        /// is not called.</exception>
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, Action<IConfigurationBuilder> configureAction)
        {
            _ = @this ?? throw new ArgumentNullException(nameof(@this));
            _ = configureAction ?? throw new ArgumentNullException(nameof(configureAction));
            var cb = new ConfigurationBuilder(@this);
            configureAction(cb);
            if (@this.Any(c => c.ServiceType == typeof(IActivityConfiguration)) == false)
                throw new InvalidOperationException($"You should provide an activity by calling {nameof(ConfigurationBuilderExtensions)}.{nameof(ConfigurationBuilderExtensions.WithActivity)}");
            @this.TryAddSingleton<INotificationStyleConfiguration, ConfigurationBuilderExtensions.NotificationStyleConfiguration>();
            @this.TryAddSingleton<IDefaultIconConfiguration, ConfigurationBuilderExtensions.DefaultIconConfiguration>();
            @this.TryAddSingleton<IChannelNameConfiguration, ConfigurationBuilderExtensions.ChannelNameConfiguration>();
            @this.TryAddSingleton<IChannelIdConfiguration, ConfigurationBuilderExtensions.ChannelIdConfiguration>();
            @this.TryAddSingleton<IChannelNotificationImportanceConfiguration, ConfigurationBuilderExtensions.ChannelNotificationImportanceConfiguration>();
            @this.TryAddSingleton<IShowBadgeConfiguration, ConfigurationBuilderExtensions.ShowBadgeConfiguration>();
            @this.TryAddSingleton<IEnableVibrationConfiguration, ConfigurationBuilderExtensions.EnableVibrationConfiguration>();
            @this.TryAddSingleton<IIntentManager, IntentManager>();
            @this.TryAddTransient(typeof(INotificationBuilder),  sp
                => sp.GetRequiredService<INotificationStyleConfiguration>().NotificationStyle.Resolve<Func<object>>(
                    sp.GetRequiredService<ISnackbarExtension>,
                    sp.GetRequiredService<IDroidNotificationExtension>)());
            @this.TryAddTransient<ISnackbarExtension, SnackbarBuilder>();
            @this.TryAddTransient<IDroidNotificationExtension, NotificationBuilder>();
            if (AndroidPlatform.IsM)
                @this.TryAddSingleton<IAndroidNotificationManager, AndroidNotificationManagerM>();
            else if (AndroidPlatform.IsEclair)
                @this.TryAddSingleton<IAndroidNotificationManager, AndroidNotificationManagerEclair>();
            else
                @this.TryAddSingleton<IAndroidNotificationManager, AndroidNotificationManager>();
            @this.TryAddSingleton<IHistory, History>();
            @this.TryAddTransient<IBigTextStyle, BigTextStyleBuilder>();
            @this.TryAddTransient<IInboxStyle, InboxStyleBuilder>();
            return @this.AddBase();
        }

        /// <summary>
        /// Add the notification manager and other services to the service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IInitialization"/> with dummy implementation,<br/>
        /// <seealso cref="INotificationManager"/>,<br/>
        /// <seealso cref="IIntentManager"/>,<br/>
        /// <seealso cref="IAndroidNotificationManager"/>,<br/>
        /// <seealso cref="ISystemEventSource"/>,<br/>
        /// <seealso cref="INotificationEventSource"/>,<br/>
        /// <seealso cref="IHistory"/>,<br/>
        /// <seealso cref="IExtensionConfiguration{T}"/><br/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="configureAction">The delegate for configuring <see cref="IConfigurationBuilder"/>
        /// that provides additional information for notification manager</param>
        /// <param name="defaultPlatformConfiguration">Default configuration for <see cref="IPlatformSpecificExtension"/></param>
        /// <param name="defaultSnackbarConfiguration">Default configuration for <see cref="ISnackbarExtension"/></param>
        /// <returns>Service collection from @this parameter</returns>
        /// <exception cref="InvalidOperationException">When
        /// <see cref="ConfigurationBuilderExtensions.WithActivity(IConfigurationBuilder, Activity)"/> or
        /// <see cref="ConfigurationBuilderExtensions.WithActivity(IConfigurationBuilder, Func{IServiceProvider, Activity})"/>
        /// is not called.</exception>
        public static IServiceCollection AddNotificationManager(
            this IServiceCollection @this,
            Action<IConfigurationBuilder> configureAction,
            Action<IPlatformSpecificExtension> defaultPlatformConfiguration,
            Action<ISnackbarExtension> defaultSnackbarConfiguration)
        {
            _ = defaultPlatformConfiguration ?? throw new ArgumentNullException(nameof(defaultPlatformConfiguration));
            _ = defaultSnackbarConfiguration ?? throw new ArgumentNullException(nameof(defaultSnackbarConfiguration));
            return @this.AddNotificationManager(configureAction)
                .AddSingleton<IExtensionConfiguration<IPlatformSpecificExtension>>(
                sp => new DefaultConfiguration<IPlatformSpecificExtension>(defaultPlatformConfiguration))
                .AddSingleton<IExtensionConfiguration<ISnackbarExtension>>(
                sp => new DefaultConfiguration<ISnackbarExtension>(defaultSnackbarConfiguration));
        }


        /// <summary>
        /// Add the notification manager and other services to the service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IInitialization"/> with dummy implementation,<br/>
        /// <seealso cref="INotificationManager"/>,<br/>
        /// <seealso cref="IIntentManager"/>,<br/>
        /// <seealso cref="IAndroidNotificationManager"/>,<br/>
        /// <seealso cref="ISystemEventSource"/>,<br/>
        /// <seealso cref="INotificationEventSource"/>,<br/>
        /// <seealso cref="IHistory"/>,<br/>
        /// <seealso cref="IExtensionConfiguration{T}"/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="configureAction">The delegate for configuring <see cref="IConfigurationBuilder"/>
        /// that provides additional information for notification manager</param>
        /// <param name="defaultPlatformConfiguration">Default configuration for <see cref="IPlatformSpecificExtension"/></param>
        /// <returns>Service collection from @this parameter</returns>
        /// <exception cref="InvalidOperationException">When
        /// <see cref="ConfigurationBuilderExtensions.WithActivity(IConfigurationBuilder, Activity)"/> or
        /// <see cref="ConfigurationBuilderExtensions.WithActivity(IConfigurationBuilder, Func{IServiceProvider, Activity})"/>
        /// is not called.</exception>
        public static IServiceCollection AddNotificationManager(
            this IServiceCollection @this,
            Action<IConfigurationBuilder> configureAction,
            Action<IPlatformSpecificExtension> defaultPlatformConfiguration)
        {
            _ = defaultPlatformConfiguration ?? throw new ArgumentNullException(nameof(defaultPlatformConfiguration));
            return @this.AddNotificationManager(configureAction)
                .AddSingleton<IExtensionConfiguration<IPlatformSpecificExtension>>(
                sp => new DefaultConfiguration<IPlatformSpecificExtension>(defaultPlatformConfiguration));
        }

        /// <summary>
        /// Add the notification manager and other services to the service collection. 
        /// Services to be added to the collection:
        /// <seealso cref="IInitialization"/> with dummy implementation,<br/>
        /// <seealso cref="INotificationManager"/>,<br/>
        /// <seealso cref="IIntentManager"/>,<br/>
        /// <seealso cref="IAndroidNotificationManager"/>,<br/>
        /// <seealso cref="ISystemEventSource"/>,<br/>
        /// <seealso cref="INotificationEventSource"/>,<br/>
        /// <seealso cref="IHistory"/>,<br/>
        /// <seealso cref="IExtensionConfiguration{T}"/>
        /// </summary>
        /// <param name="this">Service collection</param>
        /// <param name="configureAction">The delegate for configuring <see cref="IConfigurationBuilder"/>
        /// that provides additional information for notification manager</param>
        /// <param name="defaultSnackbarConfiguration">Default configuration for <see cref="ISnackbarExtension"/></param>
        /// <returns>Service collection from @this parameter</returns>
        /// <exception cref="InvalidOperationException">When
        /// <see cref="ConfigurationBuilderExtensions.WithActivity(IConfigurationBuilder, Activity)"/> or
        /// <see cref="ConfigurationBuilderExtensions.WithActivity(IConfigurationBuilder, Func{IServiceProvider, Activity})"/>
        /// is not called.</exception>
        public static IServiceCollection AddNotificationManager(
            this IServiceCollection @this,
            Action<IConfigurationBuilder> configureAction,
            Action<ISnackbarExtension> defaultSnackbarConfiguration)
        {
            _ = defaultSnackbarConfiguration ?? throw new ArgumentNullException(nameof(defaultSnackbarConfiguration));
            return @this.AddNotificationManager(configureAction)
                .AddSingleton<IExtensionConfiguration<ISnackbarExtension>>(
                sp => new DefaultConfiguration<ISnackbarExtension>(defaultSnackbarConfiguration));
        }
    }
}

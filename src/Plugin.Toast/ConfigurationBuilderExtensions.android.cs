using Android.App;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plugin.Toast.Droid.Configuration;
using System;

namespace Plugin.Toast
{
    /// <summary/>
    public static partial class ConfigurationBuilderExtensions
    {
        internal sealed record class ActivityConfiguration(Activity Activity) : IActivityConfiguration;

        /// <summary>
        /// Set an activity that:
        /// <list type="bullet">
        /// <item>will be activated in response to user interaction with the Android notification;</item>
        /// <item>used to show a snackbar notification.</item>
        /// </list>
        /// </summary>
        public static IConfigurationBuilder WithActivity(
            this IConfigurationBuilder @this,
            Activity activity)
        {
            @this.Services.TryAddSingleton<IActivityConfiguration>(
                sp => new ActivityConfiguration(activity));
            return @this;
        }

        /// <summary>
        /// Set an activity that:
        /// <list type="bullet">
        /// <item>will be activated in response to user interaction with the Android notification;</item>
        /// <item>used to show a snackbar notification.</item>
        /// </list>
        /// </summary>
        public static IConfigurationBuilder WithActivity(
            this IConfigurationBuilder @this,
            Func<IServiceProvider, Activity> getActivity)
        {
            @this.Services.TryAddSingleton<IActivityConfiguration>(
                sp => new ActivityConfiguration(getActivity(sp)));
            return @this;
        }

        internal sealed record class NotificationStyleConfiguration(NotificationStyle NotificationStyle = NotificationStyle.Default)
            : INotificationStyleConfiguration;

        /// <summary>
        /// Notification style selected by <seealso cref="INotificationManager.GetBuilder()"/>.
        /// Default value is <seealso cref="NotificationStyle.Default"/>
        /// </summary>
        public static IConfigurationBuilder WithNotificationStyle(
            this IConfigurationBuilder @this,
            NotificationStyle notificationStyle)
        {
            @this.Services.TryAddSingleton<INotificationStyleConfiguration>(
                sp => new NotificationStyleConfiguration(notificationStyle));
            return @this;
        }

        /// <summary>
        /// Notification style selected by <seealso cref="INotificationManager.GetBuilder()"/>.
        /// Default value is <seealso cref="NotificationStyle.Default"/>
        /// </summary>
        public static IConfigurationBuilder WithNotificationStyle(
            this IConfigurationBuilder @this,
            Func<IServiceProvider, NotificationStyle> getNotificationStyle)
        {
            @this.Services.TryAddSingleton<INotificationStyleConfiguration>(
                sp => new NotificationStyleConfiguration(getNotificationStyle(sp)));
            return @this;
        }

        internal sealed record class DefaultIconConfiguration(int DefaultIconId = global::Android.Resource.Drawable.IcDialogInfo)
            : IDefaultIconConfiguration;

        /// <summary>
        /// The default drawable icon for the small icon on the notification, using <see cref="IDroidNotificationExtension"/>.
        /// Not applicable to <seealso cref="ISnackbarExtension"/>. Default value is 
        /// <seealso cref="global::Android.Resource.Drawable.IcDialogInfo"/>
        /// </summary>
        public static IConfigurationBuilder WithDefaultIconId(
            this IConfigurationBuilder @this,
            int defaultIconId)
        {
            @this.Services.TryAddSingleton<IDefaultIconConfiguration>(
                sp => new DefaultIconConfiguration(defaultIconId));
            return @this;
        }

        /// <summary>
        /// The default drawable icon for the small icon on the notification, using <see cref="IDroidNotificationExtension"/>.
        /// Not applicable to <seealso cref="ISnackbarExtension"/>. Default value is 
        /// <seealso cref="global::Android.Resource.Drawable.IcDialogInfo"/>
        /// </summary>
        public static IConfigurationBuilder WithDefaultIconId(
            this IConfigurationBuilder @this,
            Func<IServiceProvider, int> getDefaultIconId)
        {
            @this.Services.TryAddSingleton<IDefaultIconConfiguration>(
                sp => new DefaultIconConfiguration(getDefaultIconId(sp)));
            return @this;
        }

        internal sealed record class PackageNameConfiguration(string PackageName) : IPackageNameConfiguration
        {
            public PackageNameConfiguration() : this(Application.Context.PackageName ?? "") { }
        }


        /// <summary>
        /// Aplication package name. By default it is Application.Context.PackageName. See also:<br/>
        /// <seealso cref="global::Android.App.Application.Context"/> <br/>
        /// <seealso cref="global::Android.Content.Context.PackageName"/>
        /// </summary>
        public static IConfigurationBuilder WithPackageName(
            this IConfigurationBuilder @this,
            string packageName)
        {
            @this.Services.TryAddSingleton<IPackageNameConfiguration>(
                sp => new PackageNameConfiguration(packageName));
            return @this;
        }

        /// <summary>
        /// Aplication package name. By default it is Application.Context.PackageName. See also:<br/>
        /// <seealso cref="global::Android.App.Application.Context"/> <br/>
        /// <seealso cref="global::Android.Content.Context.PackageName"/>
        /// </summary>
        public static IConfigurationBuilder WithPackageName(
            this IConfigurationBuilder @this,
            Func<IServiceProvider, string> getPackageName)
        {
            @this.Services.TryAddSingleton<IPackageNameConfiguration>(
                sp => new PackageNameConfiguration(getPackageName(sp)));
            return @this;
        }

        internal sealed record class ChannelNameConfiguration(string Name = "default")
            : IChannelNameConfiguration;

        /// <summary>
        /// Channel name. Default value is "default".
        /// </summary>
        public static IConfigurationBuilder WithChannelName(
            this IConfigurationBuilder @this,
            string name)
        {
            @this.Services.TryAddSingleton<IChannelNameConfiguration>(
                sp => new ChannelNameConfiguration(name));
            return @this;
        }

        /// <summary>
        /// Channel name. Default value is "default".
        /// </summary>
        public static IConfigurationBuilder WithChannelName(
            this IConfigurationBuilder @this,
            Func<IServiceProvider, string> getName)
        {
            @this.Services.TryAddSingleton<IChannelNameConfiguration>(
                sp => new ChannelNameConfiguration(getName(sp)));
            return @this;
        }

        internal sealed record class ChannelIdConfiguration(string Id = "default") : IChannelIdConfiguration;

        /// <summary>
        /// Channel id. Default value is "default".
        /// </summary>
        public static IConfigurationBuilder WithChannelId(
            this IConfigurationBuilder @this,
            string id)
        {
            @this.Services.TryAddSingleton<IChannelIdConfiguration>(
                sp => new ChannelIdConfiguration(id));
            return @this;
        }

        /// <summary>
        /// Channel id. Default value is "default".
        /// </summary>
        public static IConfigurationBuilder WithChannelId(
            this IConfigurationBuilder @this,
            Func<IServiceProvider, string> getId)
        {
            @this.Services.TryAddSingleton<IChannelIdConfiguration>(
                sp => new ChannelIdConfiguration(getId(sp)));
            return @this;
        }

        internal sealed record class ChannelNotificationImportanceConfiguration(
            DroidNotificationImportance NotificationImportance = DroidNotificationImportance.High)
            : IChannelNotificationImportanceConfiguration;

        /// <summary>
        /// Notification importance of channel. Default value is <see cref="DroidNotificationImportance.High"/>.
        /// </summary>
        public static IConfigurationBuilder WithNotificationImportance(
            this IConfigurationBuilder @this,
            DroidNotificationImportance importance)
        {
            @this.Services.TryAddSingleton<IChannelNotificationImportanceConfiguration>(
                sp => new ChannelNotificationImportanceConfiguration(importance));
            return @this;
        }

        /// <summary>
        /// Notification importance of channel. Default value is <see cref="DroidNotificationImportance.High"/>.
        /// </summary>
        public static IConfigurationBuilder WithNotificationImportance(
            this IConfigurationBuilder @this,
            Func<IServiceProvider, DroidNotificationImportance> getImportance)
        {
            @this.Services.TryAddSingleton<IChannelNotificationImportanceConfiguration>(
                sp => new ChannelNotificationImportanceConfiguration(getImportance(sp)));
            return @this;
        }

        internal sealed record class ShowBadgeConfiguration(bool ShowBadge = true) : IShowBadgeConfiguration;

        /// <summary>
        /// Show application icon badges in Launcher. Default value is true.
        /// </summary>
        public static IConfigurationBuilder WithBadge(
            this IConfigurationBuilder @this,
            bool showBadge)
        {
            @this.Services.TryAddSingleton<IShowBadgeConfiguration>(sp => new ShowBadgeConfiguration(showBadge));
            return @this;
        }

        /// <summary>
        /// Show application icon badges in Launcher. Default value is true.
        /// </summary>
        public static IConfigurationBuilder WithBadge(
            this IConfigurationBuilder @this,
            Func<IServiceProvider, bool> getShowBadge)
        {
            @this.Services.TryAddSingleton<IShowBadgeConfiguration>(sp => new ShowBadgeConfiguration(getShowBadge(sp)));
            return @this;
        }

        internal sealed record class EnableVibrationConfiguration(bool EnableVibration = true) : IEnableVibrationConfiguration;

        /// <summary>
        /// Enable vibration for channel. Default value is true.
        /// </summary>
        public static IConfigurationBuilder WithVibration(
            this IConfigurationBuilder @this,
            bool enableVibration)
        {
            @this.Services.TryAddSingleton<IEnableVibrationConfiguration>(sp => new EnableVibrationConfiguration(enableVibration));
            return @this;
        }

        /// <summary>
        /// Enable vibration for channel. Default value is true.
        /// </summary>
        public static IConfigurationBuilder WithVibration(
            this IConfigurationBuilder @this,
            Func<IServiceProvider, bool> getEnableVibration)
        {
            @this.Services.TryAddSingleton<IEnableVibrationConfiguration>(sp => new EnableVibrationConfiguration(getEnableVibration(sp)));
            return @this;
        }
    }
}

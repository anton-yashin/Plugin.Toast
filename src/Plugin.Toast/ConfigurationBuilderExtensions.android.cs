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
    }
}

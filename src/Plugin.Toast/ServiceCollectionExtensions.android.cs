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
    }
}

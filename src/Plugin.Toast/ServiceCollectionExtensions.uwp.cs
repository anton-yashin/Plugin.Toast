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
            return @this.AddBase();
        }
    }
}

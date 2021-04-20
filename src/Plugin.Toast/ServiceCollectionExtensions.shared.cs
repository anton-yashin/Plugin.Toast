using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Plugin.Toast
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddBase(this IServiceCollection @this)
        {
            @this.TryAddSingleton<IInitialization, NoInitialization>();
            @this.TryAddSingleton<INotificationManager, DiNm>();
            @this.TryAddSingleton<ISystemEventSource, SystemEventSource>();
            @this.TryAddTransient<INotificationEventSource, NotificationEventProxy>();
            @this.TryAddTransient(typeof(INotificationBuilder<>), typeof(NotificationBuilderFactory<>));
            @this.TryAddTransient(typeof(INotificationBuilder<,>), typeof(NotificationBuilderFactory<,>));
            @this.TryAddTransient(typeof(INotificationBuilder<,,>), typeof(NotificationBuilderFactory<,,>));
            @this.TryAddTransient(typeof(INotificationBuilder<,,,>), typeof(NotificationBuilderFactory<,,,>));
            return @this;
        }
    }
}

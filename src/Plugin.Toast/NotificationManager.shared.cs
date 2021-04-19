using Plugin.Toast.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    /// <summary>
    /// Non DI implementation of <see cref="INotificationManager"/>
    /// </summary>
    public sealed partial class NotificationManager : INotificationManager
    {
        private static NotificationManager? instance = null;

        /// <summary>
        /// Singleton instance of <see cref="NotificationManager"/>
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <see cref="InitializeAsync"/> is not called
        /// </exception>
        public static INotificationManager Instance => instance ?? throw Exceptions.ExceptionUtils.PleaseCallInit;
        /// <summary>
        /// Singleton instance of <see cref="IHistory"/>
        /// </summary>
        public static IHistory History => PlatformGetHistory();
        /// <summary>
        /// Creates a proxy to <see cref="ISystemEventSource"/> singleton, that
        /// you can use to subscribe on <see cref="NotificationEvent"/>.
        /// </summary>
        /// <returns>New instance of <see cref="INotificationEventSource"/></returns>
        public static INotificationEventSource GetNotificationEventSource()
            => new NotificationEventProxy(PlatformGetSystemEventSource());

        /// <inheritdoc/>
        public INotificationBuilder GetBuilder() => PlatformBuildNotification();

        /// <inheritdoc/>
        public Task InitializeAsync() => PlatformInitializeAsync();

        INotificationBuilder BuildNotificationUsing(Type t1) => PlatformResolve(t1) ?? PlatformBuildNotification();
        INotificationBuilder BuildNotificationUsing(Type t1, Type t2) => PlatformResolve(t1) ?? PlatformResolve(t2) ?? PlatformBuildNotification();
        INotificationBuilder BuildNotificationUsing(params Type[] types)
            => (from i in types let j = PlatformResolve(i) where j != null select j).FirstOrDefault()
            ?? PlatformBuildNotification();

        /// <inheritdoc/>
        public INotificationBuilder GetBuilder<T1>()
            where T1 : INotificationBuilderExtension<T1>
            => BuildNotificationUsing(typeof(T1));

        /// <inheritdoc/>
        public INotificationBuilder GetBuilder<T1, T2>() 
            where T1 : INotificationBuilderExtension<T1>
            where T2 : INotificationBuilderExtension<T2>
            => BuildNotificationUsing(typeof(T1), typeof(T2));

        /// <inheritdoc/>
        public INotificationBuilder GetBuilder<T1, T2, T3>()
            where T1 : INotificationBuilderExtension<T1>
            where T2 : INotificationBuilderExtension<T2>
            where T3 : INotificationBuilderExtension<T3>
            => BuildNotificationUsing(typeof(T1), typeof(T2), typeof(T3));

        /// <inheritdoc/>
        public INotificationBuilder GetBuilder<T1, T2, T3, T4>()
            where T1 : INotificationBuilderExtension<T1>
            where T2 : INotificationBuilderExtension<T2>
            where T3 : INotificationBuilderExtension<T3>
            where T4 : INotificationBuilderExtension<T4>
            => BuildNotificationUsing(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
    }
}

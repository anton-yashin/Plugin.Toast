using Plugin.Toast.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    public sealed partial class NotificationManager : INotificationManager
    {
        private static NotificationManager? instance = null;

        public static INotificationManager Instance => instance ?? throw Exceptions.ExceptionUtils.PleaseCallInit;
        public static IHistory History => PlatformGetHistory();
        public static INotificationEventSource GetNotificationEventSource()
            => new NotificationEventProxy(PlatformGetSystemEventSource());

        public INotificationBuilder GetBuilder() => PlatformBuildNotification();

        public Task InitializeAsync() => PlatformInitializeAsync();

        INotificationBuilder BuildNotificationUsing(Type t1) => PlatformResolve(t1) ?? PlatformBuildNotification();
        INotificationBuilder BuildNotificationUsing(Type t1, Type t2) => PlatformResolve(t1) ?? PlatformResolve(t2) ?? PlatformBuildNotification();
        INotificationBuilder BuildNotificationUsing(params Type[] types)
            => (from i in types let j = PlatformResolve(i) where j != null select j).FirstOrDefault()
            ?? PlatformBuildNotification();

        public INotificationBuilder GetBuilder<T1>()
            where T1 : INotificationBuilderExtension<T1>
            => BuildNotificationUsing(typeof(T1));

        public INotificationBuilder GetBuilder<T1, T2>() 
            where T1 : INotificationBuilderExtension<T1>
            where T2 : INotificationBuilderExtension<T2>
            => BuildNotificationUsing(typeof(T1), typeof(T2));

        public INotificationBuilder GetBuilder<T1, T2, T3>()
            where T1 : INotificationBuilderExtension<T1>
            where T2 : INotificationBuilderExtension<T2>
            where T3 : INotificationBuilderExtension<T3>
            => BuildNotificationUsing(typeof(T1), typeof(T2), typeof(T3));

        public INotificationBuilder GetBuilder<T1, T2, T3, T4>()
            where T1 : INotificationBuilderExtension<T1>
            where T2 : INotificationBuilderExtension<T2>
            where T3 : INotificationBuilderExtension<T3>
            where T4 : INotificationBuilderExtension<T4>
            => BuildNotificationUsing(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
    }
}

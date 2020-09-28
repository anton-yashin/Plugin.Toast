using System;
using System.Linq;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    public sealed partial class NotificationManager : INotificationManager
    {
        private static INotificationManager? instance = null;

        public static INotificationManager Instance => instance ?? throw new InvalidOperationException("please call init");


        public IBuilder GetBuilder() => PlatformBuildNotification();

        public Task InitializeAsync() => PlatformInitializeAsync();

        IBuilder BuildNotificationUsing(Type t1) => PlatformResolve(t1) ?? PlatformBuildNotification();
        IBuilder BuildNotificationUsing(Type t1, Type t2) => PlatformResolve(t1) ?? PlatformResolve(t2) ?? PlatformBuildNotification();
        IBuilder BuildNotificationUsing(params Type[] types)
            => (from i in types let j = PlatformResolve(i) where j != null select j).FirstOrDefault()
            ?? PlatformBuildNotification();

        public IBuilder GetBuilder<T1>()
            where T1 : IBuilderExtension<T1>
            => BuildNotificationUsing(typeof(T1));

        public IBuilder GetBuilder<T1, T2>() 
            where T1 : IBuilderExtension<T1>
            where T2 : IBuilderExtension<T2>
            => BuildNotificationUsing(typeof(T1), typeof(T2));

        public IBuilder GetBuilder<T1, T2, T3>()
            where T1 : IBuilderExtension<T1>
            where T2 : IBuilderExtension<T2>
            where T3 : IBuilderExtension<T3>
            => BuildNotificationUsing(typeof(T1), typeof(T2), typeof(T3));

        public IBuilder GetBuilder<T1, T2, T3, T4>()
            where T1 : IBuilderExtension<T1>
            where T2 : IBuilderExtension<T2>
            where T3 : IBuilderExtension<T3>
            where T4 : IBuilderExtension<T4>
            => BuildNotificationUsing(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
    }
}

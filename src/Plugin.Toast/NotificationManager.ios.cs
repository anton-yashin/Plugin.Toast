using System;
using System.Threading.Tasks;
using UIKit;
using Plugin.Toast.IOS;

namespace Plugin.Toast
{
    public sealed partial class NotificationManager
    {
        private readonly Type notificationExtension;
        private readonly Type localNotificationExtension;
        private readonly IToastOptions options;
        private readonly INotificationReceiver notificationReceiver;
        private readonly IPermission permission;
        private readonly ISystemEventSource systemEventSource;
        private readonly IHistory history;

        internal NotificationManager() : this(new ToastOptions()) { }
        internal NotificationManager(IToastOptions options)
            : this(options, new Permission(options))
        { }

        internal NotificationManager(IToastOptions options, IPermission permission) 
        {
            this.notificationExtension = typeof(IIosNotificationExtension);
            this.localNotificationExtension = typeof(IIosLocalNotificationExtension);
            this.options = options;
            this.systemEventSource = new SystemEventSource(null);
            this.notificationReceiver = new NotificationReceiver(systemEventSource);
            this.permission = permission;
            this.history = new History();
        }

        public static void Init() => instance = new NotificationManager();
        public static void Init(IToastOptions options) => instance = new NotificationManager(options);

        IBuilder PlatformBuildNotification()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
                return new NotificationBuilder(options, notificationReceiver, permission, null);
            return new LocalNotificationBuilder(null);
        }

        IBuilder? PlatformResolve(Type extensionType)
        {
            if (extensionType == notificationExtension)
                return new NotificationBuilder(options, notificationReceiver, permission, null);
            if (extensionType == localNotificationExtension)
                return new LocalNotificationBuilder(null);
            return null;
        }

        Task PlatformInitializeAsync() => permission.RequestAuthorizationAsync();
        static IHistory PlatformGetHistory() => instance?.history ?? throw Exceptions.ExceptionUtils.PleaseCallInit;
        static ISystemEventSource PlatformGetSystemEventSource() => instance?.systemEventSource ?? throw Exceptions.ExceptionUtils.PleaseCallInit;
    }
}

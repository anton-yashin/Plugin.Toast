using System;
using System.Threading.Tasks;
using UIKit;
using Plugin.Toast.IOS;
using Plugin.Toast.Abstractions;

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

        /// <summary>
        /// Initializes a singleton <see cref="Instance"/>.
        /// </summary>
        public static void Init() => instance = new NotificationManager();
        /// <summary>
        /// Initializes a singleton <see cref="Instance"/>.
        /// </summary>
        /// <param name="options">Platform defaults.</param>
        public static void Init(IToastOptions options) => instance = new NotificationManager(options);

        INotificationBuilder PlatformBuildNotification()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
                return new NotificationBuilder(options, notificationReceiver, permission, null);
            return new LocalNotificationBuilder(null);
        }

        INotificationBuilder? PlatformResolve(Type extensionType)
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

using System;
using System.Threading.Tasks;
using Plugin.Toast.Abstractions;
using Plugin.Toast.UWP;

namespace Plugin.Toast
{
    public sealed partial class NotificationManager
    {
        private readonly IToastOptions options;
        private readonly ISystemEventSource systemEventSource;
        private readonly IHistory history;

        internal NotificationManager() : this(new ToastOptions())
        { }

        internal NotificationManager(IToastOptions options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.systemEventSource = new SystemEventSource(null);
            this.history = new History();
        }

        public static void Init() => instance = new NotificationManager();
        public static void Init(IToastOptions options) => instance = new NotificationManager(options);

        IBuilder PlatformBuildNotification() => new NotificationBuilder(null);
        IBuilder? PlatformResolve(Type _) => new NotificationBuilder(null);
        Task PlatformInitializeAsync() => Task.CompletedTask;
        static IHistory PlatformGetHistory() => instance?.history ?? throw Exceptions.ExceptionUtils.PleaseCallInit;
        static ISystemEventSource PlatformGetSystemEventSource() => instance?.systemEventSource ?? throw Exceptions.ExceptionUtils.PleaseCallInit;
    }
}

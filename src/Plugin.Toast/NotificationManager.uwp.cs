using System;
using System.Threading.Tasks;
using Plugin.Toast.UWP;

namespace Plugin.Toast
{
    public sealed partial class NotificationManager
    {
        private readonly IToastOptions options;
        private readonly ISystemEventSource systemEventSource;
        private static IHistory? historyInstance;

        internal NotificationManager() : this(new ToastOptions())
        { }

        internal NotificationManager(IToastOptions options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.systemEventSource = new SystemEventSource(null);
        }

        public static void Init()
        {
            historyInstance = new History();
            instance = new NotificationManager();
        }

        public static void Init(IToastOptions options)
        {
            historyInstance = new History();
            instance = new NotificationManager(options);
        }

        IBuilder PlatformBuildNotification() => new NotificationBuilder(null);
        IBuilder? PlatformResolve(Type _) => new NotificationBuilder(null);
        Task PlatformInitializeAsync() => Task.CompletedTask;
        static IHistory PlatformGetHistory() => historyInstance ?? throw Exceptions.ExceptionUtils.PleaseCallInit;
    }
}

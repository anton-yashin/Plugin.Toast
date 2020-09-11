using System;
using System.Threading.Tasks;
using Plugin.Toast.UWP;

namespace Plugin.Toast
{
    public sealed partial class NotificationManager
    {
        private readonly IToastOptions options;

        internal NotificationManager() : this(new ToastOptions())
        { }

        internal NotificationManager(IToastOptions options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public static void Init() => instance = new NotificationManager();
        public static void Init(IToastOptions options) => instance = new NotificationManager(options);

        IBuilder PlatformBuildNotification() => new NotificationBuilder(null);
        IBuilder? PlatformResolve(Type _) => new NotificationBuilder(null);
        Task PlatformInitializeAsync() => Task.CompletedTask;
    }
}

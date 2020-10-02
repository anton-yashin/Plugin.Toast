using Android.App;
using System;
using System.Threading.Tasks;
using Plugin.Toast.Droid;

namespace Plugin.Toast
{
    public sealed partial class NotificationManager
    {
        private readonly IIntentManager intentManager;
        private readonly IToastOptions options;
        private readonly Type snackbarExtension;
        private readonly Type notificationExtension;
        private readonly IAndroidHistory history;
        private static IAndroidHistory? historyInstance;

        internal NotificationManager(IToastOptions options) 
            : this(new IntentManager(options, null), options) { }

        internal NotificationManager(IIntentManager intentManager, IToastOptions options)
        {
            this.intentManager = intentManager ?? throw new ArgumentNullException(nameof(intentManager));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.snackbarExtension = typeof(ISnackbarExtension);
            this.notificationExtension = typeof(IDroidNotificationExtension);
            this.history = historyInstance ?? InitHistory(intentManager);
        }

        public static void Init(Activity activity)
            => Init(new ToastOptions(activity));

        public static void Init(IIntentManager intentManager, Activity activity)
            => Init(intentManager, new ToastOptions(activity));

        public static void Init(IToastOptions options) 
            => Init(new IntentManager(options, null), options);

        public static void Init(IIntentManager intentManager, IToastOptions options)
        {
            historyInstance = InitHistory(intentManager);
            instance = new NotificationManager(intentManager, options);
        }

        static IAndroidHistory InitHistory(IIntentManager intentManager)
        {
            if (Platform.IsM)
                return new HistoryM(intentManager);
            if (Platform.IsEclair)
                return new HistoryEclair(intentManager);
            return new History(intentManager);
        }

        IBuilder? PlatformResolve(Type extensionType)
        {
            if (extensionType == snackbarExtension)
                return CreateSnackBarBuilder();
            else if (extensionType == notificationExtension)
                return CreateDroidNotificationBuilder();
            return null;
        }

        IBuilder CreateSnackBarBuilder() => new SnackbarBuilder(options, null);
        IBuilder CreateDroidNotificationBuilder()
            => new NotificationBuilder(options, intentManager, history, null);

        IBuilder PlatformBuildNotification() => options.NotificationStyle
            .Resolve<Func<IBuilder>>(CreateSnackBarBuilder, CreateDroidNotificationBuilder)();

        Task PlatformInitializeAsync() => Task.CompletedTask;
        static IHistory PlatformGetHistory() => historyInstance ?? throw new InvalidOperationException("please call init");
    }
}

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
        private readonly IAndroidNotificationManager androidNotificationManager;
        private static IHistory? historyInstance;
        private static IAndroidNotificationManager? androidNotificationManagerInstance;

        internal NotificationManager(IToastOptions options) 
            : this(new IntentManager(options, androidNotificationManagerInstance ?? GetAndroidNotificationManager(), null), options) { }

        internal NotificationManager(IIntentManager intentManager, IToastOptions options)
        {
            this.intentManager = intentManager ?? throw new ArgumentNullException(nameof(intentManager));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.snackbarExtension = typeof(ISnackbarExtension);
            this.notificationExtension = typeof(IDroidNotificationExtension);
            this.androidNotificationManager = androidNotificationManagerInstance ?? GetAndroidNotificationManager();
        }

        public static void Init(Activity activity)
            => Init(new ToastOptions(activity));

        public static void Init(IIntentManager intentManager, Activity activity)
            => Init(intentManager, new ToastOptions(activity));

        public static void Init(IToastOptions options) 
            => Init(new IntentManager(options, GetAndroidNotificationManager(), null), options);

        public static void Init(IIntentManager intentManager, IToastOptions options)
        {
            androidNotificationManagerInstance = GetAndroidNotificationManager();
            historyInstance = GetHistory(intentManager, androidNotificationManagerInstance);
            instance = new NotificationManager(intentManager, options);
        }

        static IAndroidNotificationManager GetAndroidNotificationManager()
        {
            if (AndroidPlatform.IsM)
                return new AndroidNotificationManager();
            if (AndroidPlatform.IsEclair)
                return new AndroidNotificationManagerEclair();
            return new AndroidNotificationManagerM();
        }

        static IHistory GetHistory(IIntentManager intentManager, IAndroidNotificationManager androidNotificationManager)
            => new History(intentManager, androidNotificationManager);

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
            => new NotificationBuilder(options, intentManager, androidNotificationManager, null);

        IBuilder PlatformBuildNotification() => options.NotificationStyle
            .Resolve<Func<IBuilder>>(CreateSnackBarBuilder, CreateDroidNotificationBuilder)();

        Task PlatformInitializeAsync() => Task.CompletedTask;
        static IHistory PlatformGetHistory() => historyInstance ?? throw new InvalidOperationException("please call init");
    }
}

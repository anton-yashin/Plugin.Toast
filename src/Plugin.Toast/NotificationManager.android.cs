﻿using Android.App;
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
        private readonly ISystemEventSource systemEventSource;
        private readonly IHistory? history;

        internal NotificationManager(IToastOptions options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.snackbarExtension = typeof(ISnackbarExtension);
            this.notificationExtension = typeof(IDroidNotificationExtension);
            this.androidNotificationManager = NewAndroidNotificationManager();
            this.systemEventSource = new SystemEventSource(null);
            this.intentManager = new IntentManager(options, androidNotificationManager, systemEventSource, null);
            this.history = new History(intentManager, androidNotificationManager);
        }

        public static void Init(Activity activity)
            => Init(new ToastOptions(activity));

        public static void Init(IToastOptions options)
            => instance = new NotificationManager(options);

        static IAndroidNotificationManager NewAndroidNotificationManager()
        {
            if (AndroidPlatform.IsM)
                return new AndroidNotificationManagerM();
            if (AndroidPlatform.IsEclair)
                return new AndroidNotificationManagerEclair();
            return new AndroidNotificationManager();
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
            => new NotificationBuilder(options, intentManager, androidNotificationManager, null);

        IBuilder PlatformBuildNotification() => options.NotificationStyle
            .Resolve<Func<IBuilder>>(CreateSnackBarBuilder, CreateDroidNotificationBuilder)();

        Task PlatformInitializeAsync() => Task.CompletedTask;
        static IHistory PlatformGetHistory() => instance?.history ?? throw Exceptions.ExceptionUtils.PleaseCallInit;
    }
}

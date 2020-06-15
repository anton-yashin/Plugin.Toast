using Android.App;
using Android.Content;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ANotificationManager = Android.App.NotificationManager;

namespace Plugin.Toast.Droid
{
    sealed class IntentManager : IIntentManager
    {
        const int KInvalidId = -1;

        int idgen;
        readonly object mutex;
        readonly Dictionary<int, TaskCompletionSource<NotificationResult>> tasksByNotificationId;
        readonly IToastOptions options;
        private readonly ILogger<IntentManager>? logger;
        readonly NotificationReceiver receiver;
        readonly IntentFilter intentFilter;

        public IntentManager(IToastOptions options, IServiceProvider? serviceProvider)
        {
            this.mutex = new object();
            this.tasksByNotificationId = new Dictionary<int, TaskCompletionSource<NotificationResult>>();
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.logger = serviceProvider?.GetService<ILogger<IntentManager>>();
            this.intentFilter = new IntentFilter();
            this.receiver = new NotificationReceiver(this);

            intentFilter.AddAction(IntentConstants.KTapped);
            intentFilter.AddAction(IntentConstants.KDismissed);
            intentFilter.AddAction(IntentConstants.KScheduled);
            Application.Context.RegisterReceiver(receiver, intentFilter);
        }

        TaskCompletionSource<NotificationResult>? PopTask(int notificationId)
        {
            lock (mutex)
            {
                if (tasksByNotificationId.Remove(notificationId, out var tcs))
                    return tcs;
            }
            return null;
        }

        public PendingIntent RegisterToShowWithDelay(INotificationBuilder builder, out int notificationId)
        {
            // https://stackoverflow.com/questions/36902667/how-to-schedule-notification-in-android

            notificationId = Interlocked.Increment(ref idgen);

            if (builder.UsingCustomContentIntent == false)
            {
                var intent = new Intent(IntentConstants.KTapped);
                builder.AddCustomArgsTo(intent);

                var activity = PendingIntent.GetActivity(Application.Context, GetRequestCode(notificationId), intent, PendingIntentFlags.CancelCurrent);
                builder.SetContentIntent(activity);
            }

            var notification = builder.Build();
            var notificationIntent = new Intent(IntentConstants.KScheduled);
            notificationIntent.PutExtra(IntentConstants.KNotificationId, notificationId);
            notificationIntent.PutExtra(IntentConstants.KNotification, notification);
            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, GetRequestCode(notificationId), notificationIntent, PendingIntentFlags.CancelCurrent);
            return pendingIntent;
        }

        public TaskCompletionSource<NotificationResult> RegisterToShowImmediatly(INotificationBuilder builder, out int notificationId)
        {
            var (cdi, cci) = (builder.UsingCustomDeleteIntent, builder.UsingCustomContentIntent);
            notificationId = Interlocked.Increment(ref idgen);
            TaskCompletionSource<NotificationResult> result = new TaskCompletionSource<NotificationResult>();
            if (cdi == false)
                builder.SetDeleteIntent(CreateDeleteIntent(builder, notificationId));
            if (cci == false)
                builder.SetContentIntent(CreateContentIntent(builder, notificationId));

            lock (mutex)
                tasksByNotificationId.Add(notificationId, result);
            if (cci)
                result.TrySetResult(NotificationResult.Unknown);
            return result;
        }

        static PendingIntent CreateDeleteIntent(INotificationBuilder builder, int notificationId)
        {
            var dismissIntent = new Intent(IntentConstants.KDismissed);
            dismissIntent.PutExtra(IntentConstants.KNotificationId, notificationId);

            var pendingDismissIntent = PendingIntent.GetBroadcast(Application.Context, GetRequestCode(notificationId), dismissIntent, 0);
            return pendingDismissIntent;
        }

        static PendingIntent CreateContentIntent(INotificationBuilder builder, int notificaionId)
        {
            var intent = new Intent(IntentConstants.KTapped);
            intent.PutExtra(IntentConstants.KNotificationId, notificaionId);
            intent.PutExtra(IntentConstants.KForceOpen, builder.GetForceOpenAppOnNotificationTap());

            builder.AddCustomArgsTo(intent);

            var result = PendingIntent.GetBroadcast(Application.Context, GetRequestCode(notificaionId), intent, 0);
            return result;
        }

        static int GetRequestCode(int notificationId) => 123 + notificationId;

        sealed class NotificationReceiver : BroadcastReceiver
        {
            private readonly HiddenReference<IntentManager> hrIntentManager;

            public NotificationReceiver(IntentManager intentManager)
            {
                hrIntentManager = new HiddenReference<IntentManager>(intentManager);
            }

            public override void OnReceive(Context context, Intent intent)
            {
                int notificationId = intent.Extras.GetInt(IntentConstants.KNotificationId, KInvalidId);
                if (notificationId == KInvalidId)
                    return;
                var tcs = hrIntentManager.Value.PopTask(notificationId);
                switch (intent.Action)
                {
                    case IntentConstants.KTapped:
                        OnTapped(intent, tcs);
                        break;
                    case IntentConstants.KScheduled:
                        OnScheduled(context, intent);
                        break;

                    default:
                        tcs?.TrySetResult(NotificationResult.UserCanceled);
                        break;
                }

            }

            private void OnTapped(Intent intent, TaskCompletionSource<NotificationResult>? tcs)
            {
                var doForceOpen = intent.Extras.GetBoolean(IntentConstants.KForceOpen, false);
                if (doForceOpen)
                {
                    try
                    {
                        var packageManager = Application.Context.PackageManager;
                        Intent launchIntent = packageManager.GetLaunchIntentForPackage(hrIntentManager.Value.options.PackageName);
                        if (launchIntent != null)
                        {
                            launchIntent.AddCategory(Intent.CategoryLauncher);
                            Application.Context.StartActivity(launchIntent);
                        }
                    }
                    catch (Exception ex)
                    {
                        hrIntentManager.Value.logger?.LogError(ex, "can't start application activity");
                    }
                }
                tcs?.TrySetResult(NotificationResult.Activated);
            }

            private void OnScheduled(Context context, Intent intent)
            {
                int notificationId = intent.GetIntExtra(IntentConstants.KNotificationId, KInvalidId);
                if (notificationId != KInvalidId)
                {
                    var notification = (global::Android.App.Notification)intent.GetParcelableExtra(IntentConstants.KNotification);
                    var nm = ANotificationManager.FromContext(context);
                    nm.Notify(notificationId, notification);
                }
            }
        }
    }
}

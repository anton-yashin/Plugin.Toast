using Android.App;
using Android.Content;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Org.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast.Droid
{
    sealed class IntentManager : IIntentManager
    {
        readonly object mutex;
        readonly Dictionary<ToastId, TaskCompletionSource<NotificationResult>> tasksByNotificationId;
        readonly IToastOptions options;
        private readonly IAndroidNotificationManager androidNotificationManager;
        private readonly ISystemEventSource systemEventSource;
        private readonly ILogger<IntentManager>? logger;
        readonly NotificationReceiver receiver;
        readonly IntentFilter intentFilter;

        public IntentManager(
            IToastOptions options,
            IAndroidNotificationManager androidNotificationManager,
            ISystemEventSource systemEventSource,
            IServiceProvider? serviceProvider)
        {
            this.mutex = new object();
            this.tasksByNotificationId = new Dictionary<ToastId, TaskCompletionSource<NotificationResult>>();
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.androidNotificationManager = androidNotificationManager;
            this.systemEventSource = systemEventSource;
            this.logger = serviceProvider?.GetService<ILogger<IntentManager>>();
            this.intentFilter = new IntentFilter();
            this.receiver = new NotificationReceiver(this);

            intentFilter.AddAction(IntentConstants.KTapped);
            intentFilter.AddAction(IntentConstants.KDismissed);
            intentFilter.AddAction(IntentConstants.KScheduled);

            Application.Context.RegisterReceiver(receiver, intentFilter);
        }

        TaskCompletionSource<NotificationResult>? PopTask(ToastId toastId)
        {
            lock (mutex)
            {
                if (tasksByNotificationId.Remove(toastId, out var tcs))
                    return tcs;
            }
            return null;
        }

        public PendingIntent RegisterToShowWithDelay(INotificationBuilder builder, ToastId toastId)
        {
            if (builder.UsingCustomContentIntent == false)
                SetContentIntent(builder, toastId);

            var notification = builder.Build();
            var intent = new Intent(IntentConstants.KScheduled);
            toastId.ToIntent(intent);
            intent.PutExtra(IntentConstants.KNotification, notification);
            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, toastId.GetPersistentHashCode(), intent, PendingIntentFlags.CancelCurrent)
                ?? throw new InvalidOperationException(ErrorStrings.KBroadcastError);
            return pendingIntent;
        }


        public PendingIntent? GetPendingIntentById(ToastId toastId)
            => PendingIntent.GetBroadcast(Application.Context, toastId.GetPersistentHashCode(), new Intent(IntentConstants.KScheduled), PendingIntentFlags.NoCreate);

        public bool IsPendingIntentExists(ToastId toastId) => GetPendingIntentById(toastId) != null;

        public TaskCompletionSource<NotificationResult> RegisterToShowImmediatly(INotificationBuilder builder, ToastId toastId)
        {
            var (cdi, cci) = (builder.UsingCustomDeleteIntent, builder.UsingCustomContentIntent);
            TaskCompletionSource<NotificationResult> result = new TaskCompletionSource<NotificationResult>();
            if (cdi == false)
                builder.SetDeleteIntent(CreateContentOrDeleteIntent(IntentConstants.KDismissed, builder, toastId));
            if (cci == false)
                SetContentIntent(builder, toastId);

            lock (mutex)
                tasksByNotificationId.Add(toastId, result);
            if (cci)
                result.TrySetResult(NotificationResult.Unknown);
            return result;
        }

        void SetContentIntent(INotificationBuilder builder, ToastId toastId)
        {
            builder.SetContentIntent(
                builder.GetForceOpenAppOnNotificationTap()
                ? CreateLaunchIntent(builder, toastId)
                : CreateContentOrDeleteIntent(IntentConstants.KTapped, builder, toastId));
        }

        static PendingIntent CreateContentOrDeleteIntent(string action, INotificationBuilder builder, ToastId toastId)
        {
            var intent = new Intent(action);
            toastId.ToIntent(intent);
            builder.AddCustomArgsTo(intent);

            var result = PendingIntent.GetBroadcast(Application.Context, toastId.GetPersistentHashCode(), intent, 0)
                ?? throw new InvalidOperationException(ErrorStrings.KBroadcastError);
            return result;
        }

        PendingIntent CreateLaunchIntent(INotificationBuilder builder, ToastId toastId)
        {
            var packageManager = Application.Context.PackageManager;
            var intent = packageManager?.GetLaunchIntentForPackage(options.PackageName);
            if (intent == null)
                throw new InvalidOperationException("can't get launch intent");
            intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTop);
            toastId.ToIntent(intent);
            builder.AddCustomArgsTo(intent);
            var result = PendingIntent.GetActivity(Application.Context, toastId.GetPersistentHashCode(), intent, PendingIntentFlags.UpdateCurrent)
                ?? throw new InvalidOperationException(ErrorStrings.KBroadcastError);
            return result;
        }

        sealed class NotificationReceiver : BroadcastReceiver
        {
            const string KTag = nameof(NotificationReceiver);
            private readonly HiddenReference<IntentManager> hrIntentManager;

            public NotificationReceiver(IntentManager intentManager)
            {
                hrIntentManager = new HiddenReference<IntentManager>(intentManager);
            }

            public override void OnReceive(Context? context, Intent? intent)
            {
                if (intent == null)
                    return;
                var toastId = ToastId.FromIntent(intent);
                if (toastId == null)
                    return;
                var tcs = hrIntentManager.Value.PopTask(toastId);
                switch (intent.Action)
                {
                    case IntentConstants.KTapped:
                        OnTapped(toastId, tcs);
                        break;
                    case IntentConstants.KScheduled:
                        OnScheduled(toastId, intent);
                        break;

                    default:
                        tcs?.TrySetResult(NotificationResult.UserCanceled);
                        break;
                }

            }

            private void OnTapped(ToastId toastId, TaskCompletionSource<NotificationResult>? tcs)
            {
                var activity = hrIntentManager.Value.options.Activity;
                if (activity.IsDestroyed)
                {
                    try
                    {
                        var packageManager = Application.Context.PackageManager;
                        var intent = packageManager?.GetLaunchIntentForPackage(hrIntentManager.Value.options.PackageName);
                        if (intent != null)
                        {
                            intent.AddCategory(Intent.CategoryLauncher);
                            toastId.ToIntent(intent);
                            Application.Context.StartActivity(intent);
                        }
                    }
                    catch (Exception ex)
                    {
                        hrIntentManager.Value.logger?.LogError(ex, "can't start application activity");
                    }
                }
                else if (activity.HasWindowFocus == false)
                {
                    try
                    {
                        var intent = new Intent(activity, activity.GetType());
                        toastId.ToIntent(intent);
                        intent.SetFlags(ActivityFlags.ReorderToFront);
                        activity.StartActivityIfNeeded(intent, 0);
                    }
                    catch (Exception ex)
                    {
                        hrIntentManager.Value.logger?.LogError(ex, "can't reorder to front an application activity");
                    }
                }
                tcs?.TrySetResult(NotificationResult.Activated);
                hrIntentManager.Value.systemEventSource.SendEvent(new NotificationEvent(toastId));
            }

            private void OnScheduled(ToastId toastId, Intent intent)
            {
                var notification = (global::Android.App.Notification?)intent.GetParcelableExtra(IntentConstants.KNotification);
                if (notification != null)
                    hrIntentManager.Value.androidNotificationManager.Notify(notification, toastId);
            }
        }
    }
}

﻿using Android.App;
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
        private readonly ILogger<IntentManager>? logger;
        readonly NotificationReceiver receiver;
        readonly IntentFilter intentFilter;

        public IntentManager(IToastOptions options, IAndroidNotificationManager androidNotificationManager, IServiceProvider? serviceProvider)
        {
            this.mutex = new object();
            this.tasksByNotificationId = new Dictionary<ToastId, TaskCompletionSource<NotificationResult>>();
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.androidNotificationManager = androidNotificationManager;
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
            // https://stackoverflow.com/questions/36902667/how-to-schedule-notification-in-android

            if (builder.UsingCustomContentIntent == false)
            {
                var intent = new Intent(IntentConstants.KTapped);
                builder.AddCustomArgsTo(intent);

                var activity = PendingIntent.GetActivity(Application.Context, toastId.GetPersistentHashCode(), intent, PendingIntentFlags.CancelCurrent)
                    ?? throw new InvalidOperationException(ErrorStrings.KActivityError);
                builder.SetContentIntent(activity);
            }

            var notification = builder.Build();
            var notificationIntent = new Intent(IntentConstants.KScheduled);
            toastId.ToIntent(notificationIntent);
            notificationIntent.PutExtra(IntentConstants.KNotification, notification);
            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, toastId.GetPersistentHashCode(), notificationIntent, PendingIntentFlags.CancelCurrent)
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
                builder.SetDeleteIntent(CreateDeleteIntent(builder, toastId));
            if (cci == false)
                builder.SetContentIntent(CreateContentIntent(builder, toastId));

            lock (mutex)
                tasksByNotificationId.Add(toastId, result);
            if (cci)
                result.TrySetResult(NotificationResult.Unknown);
            return result;
        }

        static PendingIntent CreateDeleteIntent(INotificationBuilder builder, ToastId toastId)
        {
            var dismissIntent = new Intent(IntentConstants.KDismissed);
            toastId.ToIntent(dismissIntent);

            var pendingDismissIntent = PendingIntent.GetBroadcast(Application.Context, toastId.GetPersistentHashCode(), dismissIntent, 0)
                ?? throw new InvalidOperationException(ErrorStrings.KBroadcastError);
            return pendingDismissIntent;
        }

        static PendingIntent CreateContentIntent(INotificationBuilder builder, ToastId toastId)
        {
            var intent = new Intent(IntentConstants.KTapped);
            toastId.ToIntent(intent);
            intent.PutExtra(IntentConstants.KForceOpen, builder.GetForceOpenAppOnNotificationTap());

            builder.AddCustomArgsTo(intent);

            var result = PendingIntent.GetBroadcast(Application.Context, toastId.GetPersistentHashCode(), intent, 0)
                ?? throw new InvalidOperationException(ErrorStrings.KBroadcastError);
            return result;
        }

        sealed class NotificationReceiver : BroadcastReceiver
        {
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
                        OnTapped(toastId, intent, tcs);
                        break;
                    case IntentConstants.KScheduled:
                        OnScheduled(toastId, intent);
                        break;

                    default:
                        tcs?.TrySetResult(NotificationResult.UserCanceled);
                        break;
                }

            }

            private void OnTapped(ToastId toastId, Intent intent, TaskCompletionSource<NotificationResult>? tcs)
            {
                var doForceOpen = intent.Extras?.GetBoolean(IntentConstants.KForceOpen, false) ?? false;
                if (doForceOpen)
                {
                    try
                    {
                        var packageManager = Application.Context.PackageManager;
                        var launchIntent = packageManager?.GetLaunchIntentForPackage(hrIntentManager.Value.options.PackageName);
                        if (launchIntent != null)
                        {
                            launchIntent.AddCategory(Intent.CategoryLauncher);
                            toastId.ToIntent(launchIntent);
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

            private void OnScheduled(ToastId toastId, Intent intent)
            {
                var notification = (global::Android.App.Notification?)intent.GetParcelableExtra(IntentConstants.KNotification);
                if (notification != null)
                    hrIntentManager.Value.androidNotificationManager.Notify(notification, toastId);
            }
        }
    }
}

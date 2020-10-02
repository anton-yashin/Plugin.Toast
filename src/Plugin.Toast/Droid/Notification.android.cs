using Android.App;
using Android.OS;
using System;
using System.Threading;
using System.Threading.Tasks;
using ANotificationManager = Android.App.NotificationManager;

namespace Plugin.Toast.Droid
{
    sealed class Notification : INotification
    {
        private readonly INotificationBuilder notificationBuilder;
        private readonly IIntentManager intentManager;
        private readonly IAndroidHistory history;

        public Notification(INotificationBuilder notificationBuilder, IIntentManager intentManager, IAndroidHistory history)
        {
            this.notificationBuilder = notificationBuilder ?? throw new ArgumentNullException(nameof(notificationBuilder));
            this.intentManager = intentManager;
            this.history = history;
        }

        public Task<NotificationResult> ShowAsync(out ToastId toastId, CancellationToken cancellationToken)
        {
            toastId = ToastId.New();
            return PrivateShowAsync(toastId, cancellationToken);
        }

        async Task<NotificationResult> PrivateShowAsync(ToastId toastId, CancellationToken cancellationToken)
        {
            var tcs = intentManager.RegisterToShowImmediatly(notificationBuilder, toastId);
            var notification = notificationBuilder.Build();
            var anm = ANotificationManager.FromContext(Application.Context)
                ?? throw new InvalidOperationException(ErrorStrings.KNotificationManagerError);
            using var timer = notificationBuilder.Timeout == Timeout.InfiniteTimeSpan ? null : new Timer(_ =>
            {
                if (notificationBuilder.CleanupOnTimeout)
                    history.Remove(toastId);
                tcs.TrySetResult(NotificationResult.TimedOut);
            }, null, notificationBuilder.Timeout, Timeout.InfiniteTimeSpan);
            history.Add(notification, toastId);
            if (cancellationToken.CanBeCanceled)
                return await tcs.WatchCancellationAsync(cancellationToken, () => history.Remove(toastId));
            return await tcs.Task;
        }

        public IScheduledToastCancellation ScheduleTo(DateTimeOffset deliveryTime)
        {
            var tid = ToastId.New();
            var @do = CalculateDeliveryOffset(deliveryTime);
            var pi = intentManager.RegisterToShowWithDelay(notificationBuilder, tid);

            var am = AlarmManager.FromContext(Application.Context)
                ?? throw new InvalidOperationException(ErrorStrings.KAlarmManagerError);
            am.Set(AlarmType.ElapsedRealtimeWakeup, @do, pi);

            return new AlarmCancellation(pi, tid);
        }

        long CalculateDeliveryOffset(DateTimeOffset deliveryTime)
        {
            try
            {
                checked
                {
                    var delta = (deliveryTime - DateTimeOffset.UtcNow).TotalMilliseconds;
                    return SystemClock.ElapsedRealtime() + Convert.ToInt64(delta);
                }
            }
            catch (OverflowException overflow)
            {
                throw new ArgumentOutOfRangeException(nameof(deliveryTime), overflow);
            }
        }

        sealed class AlarmCancellation : IScheduledToastCancellation
        {
            private readonly PendingIntent pendingIntent;

            public AlarmCancellation(PendingIntent pendingIntent, ToastId toastId)
            {
                this.pendingIntent = pendingIntent;
                ToastId = toastId;
            }

            public ToastId ToastId { get; }

            public void Dispose()
            {
                var am = AlarmManager.FromContext(Application.Context);
                am?.Cancel(pendingIntent);
                pendingIntent.Cancel();
            }
        }
    }
}

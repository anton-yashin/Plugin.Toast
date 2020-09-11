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

        public Notification(INotificationBuilder notificationBuilder, IIntentManager intentManager)
        {
            this.notificationBuilder = notificationBuilder ?? throw new ArgumentNullException(nameof(notificationBuilder));
            this.intentManager = intentManager;
        }

        public async Task<NotificationResult> ShowAsync(CancellationToken cancellationToken)
        {
            var tcs = intentManager.RegisterToShowImmediatly(notificationBuilder, out var notificationId);
            var notification = notificationBuilder.Build();
            var anm = ANotificationManager.FromContext(Application.Context)
                ?? throw new InvalidOperationException(ErrorStrings.KNotificationManagerError);
            using var timer = notificationBuilder.Timeout == Timeout.InfiniteTimeSpan ? null : new Timer(_ =>
            {
                if (notificationBuilder.CleanupOnTimeout)
                    anm.Cancel(notificationId);
                tcs.TrySetResult(NotificationResult.TimedOut);
            }, null, notificationBuilder.Timeout, Timeout.InfiniteTimeSpan);
            anm.Notify(notificationId, notification);
            if (cancellationToken.CanBeCanceled)
                return await tcs.WatchCancellationAsync(cancellationToken, () => anm.Cancel(notificationId));
            return await tcs.Task;
        }

        public IScheduledToastCancellation ScheduleTo(DateTimeOffset deliveryTime)
        {
            var @do = CalculateDeliveryOffset(deliveryTime);
            var pi = intentManager.RegisterToShowWithDelay(notificationBuilder, out var notificationId);

            var am = AlarmManager.FromContext(Application.Context)
                ?? throw new InvalidOperationException(ErrorStrings.KAlarmManagerError);
            am.Set(AlarmType.ElapsedRealtimeWakeup, @do, pi);

            return new AlarmCancellation(pi);
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

            public AlarmCancellation(PendingIntent pendingIntent)
            {
                this.pendingIntent = pendingIntent;
            }

            public void Dispose()
            {
                var am = AlarmManager.FromContext(Application.Context);
                am?.Cancel(pendingIntent);
            }
        }
    }
}

using Android.App;
using System;
using System.Threading.Tasks;

namespace Plugin.Toast.Droid
{
    sealed class History : IHistory
    {
        private readonly IIntentManager intentManager;
        private readonly IAndroidNotificationManager androidNotificationManager;

        public History(IIntentManager intentManager, IAndroidNotificationManager androidNotificationManager)
        {
            this.intentManager = intentManager;
            this.androidNotificationManager = androidNotificationManager;
        }

        static AlarmManager AlarmManager => Android.App.AlarmManager.FromContext(Application.Context)
            ?? throw new InvalidOperationException(ErrorStrings.KAlarmManagerError);

        public Task<bool> IsDeliveredAsync(ToastId toastId)
            => Task.FromResult(androidNotificationManager.IsDelivered(toastId));

        public Task<bool> IsScheduledAsync(ToastId toastId)
            => Task.FromResult(intentManager.IsPendingIntentExists(toastId));

        public void RemoveScheduled(ToastId toastId)
        {
            var pi = intentManager.GetPendingIntentById(toastId);
            if (pi != null)
            {
                AlarmManager.Cancel(pi);
                pi.Cancel();
            }
        }

        public void RemoveDelivered(ToastId toastId)
            => androidNotificationManager.Cancel(toastId);

        public void RemoveAllDelivered()
            => androidNotificationManager.CancelAll();
    }

}

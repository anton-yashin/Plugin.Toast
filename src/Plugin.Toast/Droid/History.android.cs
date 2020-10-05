using Android.App;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Plugin.Toast.Droid
{
    class History : IHistory, IAndroidHistory
    {
        private readonly IIntentManager intentManager;

        public History(IIntentManager intentManager)
        {
            this.intentManager = intentManager;
        }
        protected static Android.App.NotificationManager NotificationManager => Android.App.NotificationManager.FromContext(Application.Context)
                ?? throw new InvalidOperationException(ErrorStrings.KNotificationManagerError);

        protected static Android.App.AlarmManager AlarmManager => Android.App.AlarmManager.FromContext(Application.Context)
            ?? throw new InvalidOperationException(ErrorStrings.KAlarmManagerError);

        public virtual void Add(Android.App.Notification notification, ToastId toastId) 
            => NotificationManager.Notify(toastId.Id, notification);

        public virtual Task<bool> IsDeliveredAsync(ToastId toastId) => Task.FromResult(false);

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

        public virtual void Remove(ToastId toastId) 
            => NotificationManager.Cancel(toastId.Id);
        public void RemoveAll() 
            => NotificationManager.CancelAll();
    }

    class HistoryEclair : History
    {
        public HistoryEclair(IIntentManager intentManager) : base(intentManager) { }
        public override void Add(Android.App.Notification notification, ToastId toastId)
            => NotificationManager.Notify(toastId.Tag, toastId.Id, notification);
        public override void Remove(ToastId toastId)
            => NotificationManager.Cancel(toastId.Tag, toastId.Id);
    }

    sealed class HistoryM : HistoryEclair
    {
        public HistoryM(IIntentManager intentManager) : base(intentManager) { }

        public override Task<bool> IsDeliveredAsync(ToastId toastId)
            => Task.FromResult(NotificationManager.GetActiveNotifications()
                ?.Where(n => n.Tag == toastId.Tag && n.Id == toastId.Id)?.Any() == true);
    }


    interface IAndroidHistory : IHistory
    {
        void Add(global::Android.App.Notification notification, ToastId toastId);
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace Plugin.Toast.UWP
{
    sealed class History : IHistory
    {
        public History() { }

        public Task<bool> IsDeliveredAsync(ToastId toastId)
            => Task.FromResult(ToastNotificationManager.History.GetHistory()
                .Where(n => n.Tag == toastId.Tag && n.Group == toastId.Group).Any());

        public Task<bool> IsScheduledAsync(ToastId toastId)
        {
            throw new NotImplementedException("FIXME");
        }

        public void Remove(ToastId toastId)
        {
            if (string.IsNullOrEmpty(toastId.Group) == false)
                ToastNotificationManager.History.Remove(toastId.Tag, toastId.Group);
            else
                ToastNotificationManager.History.Remove(toastId.Tag);
        }

        public void RemoveAll() => ToastNotificationManager.History.Clear();
    }
}

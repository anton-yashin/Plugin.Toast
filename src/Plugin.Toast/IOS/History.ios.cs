using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    sealed class History : IHistory
    {
        public async Task<bool> IsDeliveredAsync(ToastId toastId)
        {
            var list = await UNUserNotificationCenter.Current.GetDeliveredNotificationsAsync();
            return list.Where(n => n.Request.Identifier == toastId.Id).Any();
        }

        public Task<bool> IsScheduledAsync(ToastId toastId)
        {
            throw new NotImplementedException("FIXME");
        }

        public void Remove(ToastId toastId)
            => UNUserNotificationCenter.Current.RemoveDeliveredNotifications(new string[] { toastId.Id });

        public void RemoveAll() => UNUserNotificationCenter.Current.RemoveAllDeliveredNotifications();
    }
}

using System;
using System.Threading.Tasks;

namespace Plugin.Toast.IOS
{
    public interface INotificationReceiver
    {
        IDisposable RegisterRequest(ToastId toastId, Action onShown, Action onTapped);
    }
}
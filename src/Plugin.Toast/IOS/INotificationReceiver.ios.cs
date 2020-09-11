using System;
using System.Threading.Tasks;

namespace Plugin.Toast.IOS
{
    public interface INotificationReceiver
    {
        IDisposable RegisterRequest(string id, Action onShown, Action onTapped);
    }
}
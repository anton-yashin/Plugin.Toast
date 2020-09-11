using UserNotifications;

namespace Plugin.Toast.IOS
{
    interface INotificationBuilder
    {
        UNMutableNotificationContent Notification { get; }
    }
}
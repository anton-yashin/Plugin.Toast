using UserNotifications;

namespace Plugin.Toast.IOS
{
    interface IPlatformNotificationBuilder
    {
        UNMutableNotificationContent Notification { get; }
    }
}
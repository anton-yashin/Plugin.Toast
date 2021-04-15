using Plugin.Toast.Abstractions;
using System;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    public sealed partial class NotificationManager
    {
        private NotificationManager() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
        INotificationBuilder PlatformBuildNotification() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
        INotificationBuilder? PlatformResolve(Type _) => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
        Task PlatformInitializeAsync() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
        static IHistory PlatformGetHistory() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
        static ISystemEventSource PlatformGetSystemEventSource() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
    }
}

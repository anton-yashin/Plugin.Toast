using System;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    public sealed partial class NotificationManager
    {
        private NotificationManager() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
        IBuilder PlatformBuildNotification() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
        IBuilder? PlatformResolve(Type _) => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
        Task PlatformInitializeAsync() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
        static IHistory PlatformGetHistory() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
    }
}

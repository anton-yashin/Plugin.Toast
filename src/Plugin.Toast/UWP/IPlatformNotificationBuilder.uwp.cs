using System;

namespace Plugin.Toast.UWP
{
    interface IPlatformNotificationBuilder
    {
        string? Tag { get; }
        string? Group { get; }
        string? RemoteId { get; }
        TimeSpan? SnoozeInterval { get; }
        uint MaximumSnoozeCount { get; }
        bool SuppressPopup { get; }
    }
}

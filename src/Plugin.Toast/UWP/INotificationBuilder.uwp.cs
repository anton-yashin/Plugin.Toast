using System;

namespace Plugin.Toast.UWP
{
    interface INotificationBuilder
    {
        string? Tag { get; }
        string? Group { get; }
        string? RemoteId { get; }
        TimeSpan? SnoozeInterval { get; }
        uint MaximumSnoozeCount { get; }
        bool SuppressPopup { get; }
    }
}

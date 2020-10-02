using System;
using Windows.UI.Notifications;

namespace Plugin.Toast
{
    public sealed partial class ToastId : IEquatable<ToastId>
    {
        public string Tag { get; }
        public string? Group { get; }
        public ToastIdNotificationType NotificationType { get; }

        public ToastId(string tag, string? group, ToastIdNotificationType notificationType)
        {
            Tag = tag;
            Group = group;
            NotificationType = notificationType;
        }

        public static ToastId FromNotification(ToastNotification toastNotification)
            => new ToastId(
                tag: toastNotification.Tag,
                group: toastNotification.Group,
                notificationType: ToastIdNotificationType.ToastNotification
                );

        public static ToastId FromNotification(ScheduledToastNotification toastNotification)
            => new ToastId(
                tag: toastNotification.Tag,
                group: toastNotification.Group,
                notificationType: ToastIdNotificationType.ScheduledToastNotification
                );

        (string tag, string? group, ToastIdNotificationType notificationType) AsTuple()
            => (Tag, Group, NotificationType);

        public bool Equals(ToastId? other) => other != null && AsTuple() == other.AsTuple();

        public override bool Equals(object? obj) => Equals(obj as ToastId);

        public override int GetHashCode() => AsTuple().GetHashCode();

        int GetPlatformPersistentHashCode()
            => CombineHashCode(CombineHashCode(CombineHashCode(KMagicSeed, Tag), Group), (int)NotificationType);
    }

    public enum ToastIdNotificationType
    {
        Unknown,
        ToastNotification,
        ScheduledToastNotification,
    }
}

﻿using System;
using System.Runtime.Serialization;
using Windows.UI.Notifications;

namespace Plugin.Toast
{
    public sealed partial class ToastId : IEquatable<ToastId>
    {
        string tag;
        string group;

        /// <summary>
        /// Tag part of notification identifier.
        /// </summary>
        [DataMember]
        public string Tag 
        {
            get => tag;
            [Obsolete(KObsoleteMessage, true)]
            set => tag = value;
        }

        /// <summary>
        /// Group part of notification identifier.
        /// </summary>
        [DataMember]
        public string Group
        {
            get => group;
            [Obsolete(KObsoleteMessage, true)]
            set => group = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToastId"/> class.
        /// </summary>
        /// <param name="tag">Tag part of notificaition identifier</param>
        /// <param name="group">Group part of notificaition identifier</param>
        public ToastId(string? tag, string? group)
        {
            this.tag = tag ?? "";
            this.group = group ?? "";
        }

        /// <summary>
        /// Creates <see cref="ToastId"/> from specified notification.
        /// </summary>
        /// <param name="toastNotification">Toast notification</param>
        /// <returns>ToastId</returns>
        public static ToastId FromNotification(ToastNotification toastNotification)
            => new ToastId(
                tag: toastNotification.Tag,
                group: toastNotification.Group
                );

        /// <summary>
        /// Creates <see cref="ToastId"/> from specified notification.
        /// </summary>
        /// <param name="toastNotification">Toast notification</param>
        /// <returns>ToastId</returns>
        public static ToastId FromNotification(ScheduledToastNotification toastNotification)
            => new ToastId(
                tag: toastNotification.Tag,
                group: toastNotification.Group
                );

        (string tag, string? group) AsTuple() => (Tag, Group);

        bool PlatformEquals(ToastId? other) => other != null && AsTuple() == other.AsTuple();
        bool PlatformEquals(object? obj) => PlatformEquals(obj as ToastId);
        private int PlatformGetHashCode() => AsTuple().GetHashCode();
        private string PlatformToString() => string.Format("Tag: {0}, Group: {1}", Tag, Group);

        int GetPlatformPersistentHashCode()
            => CombineHashCode(CombineHashCode(KMagicSeed, Tag), Group);
    }
}

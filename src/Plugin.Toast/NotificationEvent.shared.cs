using System;

namespace Plugin.Toast
{
    /// <summary>
    /// Notification event.
    /// </summary>
    public sealed class NotificationEvent : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationEvent"/> class.
        /// </summary>
        /// <param name="toastId"></param>
        public NotificationEvent(ToastId toastId) => ToastId = toastId;

        /// <summary>
        /// Identifier of notification linked with this event.
        /// </summary>
        public ToastId ToastId { get; }
    }
}

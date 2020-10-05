using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public sealed class NotificationEvent : EventArgs
    {
        public NotificationEvent(ToastId toastId) => ToastId = toastId;

        public ToastId ToastId { get; }
    }
}

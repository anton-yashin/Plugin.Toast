using System;

namespace Plugin.Toast
{
    /// <summary>
    /// A token to remove a notification from schedule. Call <see cref="IDisposable.Dispose"/>
    /// if you don't want to show a notification. A delivered notification can not be removed
    /// from notification center using <see cref="IDisposable.Dispose"/>
    /// </summary>
    public interface IScheduledToastCancellation : IDisposable
    {
        /// <summary>
        /// The notification id associated with this token.
        /// </summary>
        ToastId ToastId { get; }
    }
}

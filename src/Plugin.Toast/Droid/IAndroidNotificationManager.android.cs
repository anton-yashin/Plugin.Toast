using System;

namespace Plugin.Toast.Droid
{
    /// <summary>
    /// Interface for version dependent implementations of <see cref="Android.App.NotificationManager"/>
    /// </summary>
    public interface IAndroidNotificationManager
    {
        /// <summary>
        /// Post a notification to be shown in the status bar.
        /// </summary>
        /// <param name="notification">A <see cref="Android.App.Notification"/> object describing what to show the user. Must not be null.</param>
        /// <param name="toastId">An identifier for this notification unique within your application.</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        void Notify(global::Android.App.Notification notification, ToastId toastId);
        /// <summary>
        /// Cancel a previously shown notification.
        /// </summary>
        /// <param name="toastId">An identifier for this notification (unique within your application).</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        void Cancel(ToastId toastId);
        /// <summary>
        /// Cancel all previously shown notifications.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        void CancelAll();
        /// <summary>
        /// Checks if notification is in list of active notifications.
        /// </summary>
        /// <param name="toastId">An identifier for this notification (unique within your application).</param>
        /// <returns><b>True</b> if notification is in list of active notifications.</returns>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        bool IsDelivered(ToastId toastId);
    }
}

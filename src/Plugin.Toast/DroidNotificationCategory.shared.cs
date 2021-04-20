using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
#if __ANDROID__ == false
#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
#endif
    /// <summary>
    /// Notification category for <see cref="IDroidNotificationExtension.SetCategory(DroidNotificationCategory)"/>
    /// <see cref="global::Android.App.Notification"/>
    /// </summary>
    public sealed class DroidNotificationCategory
    {
#if __ANDROID__ == false
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
#endif
        private readonly string category;

        /// <summary>
        /// Initializes a new instance of the <see cref="DroidNotificationCategory"/> class.
        /// </summary>
        /// <param name="category"></param>
        public DroidNotificationCategory(string category) => this.category = category;

        /// <inheritdoc/>
        public override string ToString() => category;

        /// <summary>
        /// Notification category: alarm or timer.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Alarm { get; } = new DroidNotificationCategory("alarm");
        /// <summary>
        /// Notification category: incoming call (voice or video) or similar synchronous communication request.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Call { get; } = new DroidNotificationCategory("call");
        /// <summary>
        /// Notification category: asynchronous bulk message (email).
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Email { get; } = new DroidNotificationCategory("email");
        /// <summary>
        /// Notification category: error in background operation or authentication status.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Error { get; } = new DroidNotificationCategory("err");
        /// <summary>
        /// Notification category: calendar event.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Event { get; } = new DroidNotificationCategory("event");
        /// <summary>
        /// Notification category: temporarily sharing location.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory LocationSharing { get; } = new DroidNotificationCategory("location_sharing");
        /// <summary>
        /// Notification category: incoming direct message (SMS, instant message, etc.).
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Message { get; } = new DroidNotificationCategory("msg");
        /// <summary>
        /// Notification category: missed call.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory MissedCall { get; } = new DroidNotificationCategory("missed_call");
        /// <summary>
        /// Notification category: map turn-by-turn navigation.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Navigation { get; } = new DroidNotificationCategory("navigation");
        /// <summary>
        /// Notification category: progress of a long-running background operation.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Progress { get; } = new DroidNotificationCategory("progress");
        /// <summary>
        /// Notification category: promotion or advertisement.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Promo { get; } = new DroidNotificationCategory("promo");
        /// <summary>
        /// Notification category: a specific, timely recommendation for a single thing.
        /// For example, a news app might want to recommend a news story it believes the
        /// user will want to read next.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Recommendation { get; } = new DroidNotificationCategory("recommendation");
        /// <summary>
        /// Notification category: user-scheduled reminder.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Reminder { get; } = new DroidNotificationCategory("reminder");
        /// <summary>
        /// Notification category: indication of running background service.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Service { get; } = new DroidNotificationCategory("service");
        /// <summary>
        /// Notification category: social network or sharing update.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Social { get; } = new DroidNotificationCategory("social");
        /// <summary>
        /// Notification category: system or device status update. Reserved for system use.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory System { get; } = new DroidNotificationCategory("sys");
        /// <summary>
        /// Notification category: ongoing information about device or contextual status.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Status { get; } = new DroidNotificationCategory("status");
        /// <summary>
        /// Notification category: running stopwatch.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Stopwatch { get; } = new DroidNotificationCategory("stopwatch");
        /// <summary>
        /// Notification category: media transport control for playback.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public static DroidNotificationCategory Transport { get; } = new DroidNotificationCategory("transport");
    }
}

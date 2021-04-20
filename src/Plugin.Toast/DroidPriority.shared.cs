using System;

namespace Plugin.Toast
{
    /// <summary>
    /// Priority values for <see cref="IDroidNotificationExtension.SetPriority(DroidPriority)"/>
    /// </summary>
    public enum DroidPriority
    {
        /// <summary>
        /// Lowest notification priority for <see cref="IDroidNotificationExtension.SetPriority(DroidPriority)"/>;
        /// these items might not be shown to the user except under special circumstances, such as detailed notification logs. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Min = -2,
        /// <summary>
        /// Lower notification priority for <see cref="IDroidNotificationExtension.SetPriority(DroidPriority)"/>,
        /// for items that are less important. The UI may choose to show these items smaller, or at a different
        /// position in the list, compared with your app's <see cref="Default"/> items. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Low = -1,
        /// <summary>
        /// Default notification priority for <see cref="IDroidNotificationExtension.SetPriority(DroidPriority)"/>.
        /// If your application does not prioritize its own notifications, use this value for all notifications. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Default = 0,
        /// <summary>
        /// Higher notification priority for <see cref="IDroidNotificationExtension.SetPriority(DroidPriority)"/>,
        /// for more important notifications or alerts. The UI may choose to show these items larger, or at a
        /// different position in notification lists, compared with your app's <see cref="Default"/> items. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        High = 1,
        /// <summary>
        /// Highest notification priority for <see cref="IDroidNotificationExtension.SetPriority(DroidPriority)"/>,
        /// for your application's most important items that require the user's prompt attention or input. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Max = 2,
    }
}

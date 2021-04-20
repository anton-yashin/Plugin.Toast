using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Type of android notification badge icon.
    /// </summary>
    public enum DroidBadgeIcon
    {
        /// <summary>
        /// If this notification is being shown as a badge, always show as a number. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        None = 0,
        /// <summary>
        /// If this notification is being shown as a badge, use the icon provided
        /// to <see cref="IDroidNotificationExtension.SetSmallIcon(int)"/> to represent this notification. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Small = 1,
#if __ANDROID__ == false
#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
#endif

        /// <summary>
        /// If this notification is being shown as a badge, use the icon provided
        /// to <see cref="Droid.IPlatformSpecificExtension.SetLargeIcon(Android.Graphics.Bitmap)"/>
        /// to represent this notification. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Large = 2,
#if __ANDROID__ == false
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
#endif
    }
}

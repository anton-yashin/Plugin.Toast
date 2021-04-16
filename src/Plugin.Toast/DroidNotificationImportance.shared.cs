using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Values that represents the importance of notifications.
    /// </summary>
    public enum DroidNotificationImportance
    {
        /// <summary>
        /// Value signifying that the user has not expressed an importance.
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationManager#IMPORTANCE_UNSPECIFIED"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Unspecified = -1000,
        /// <summary>
        /// A notification with no importance: does not show in the shade.
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationManager#IMPORTANCE_NONE"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        None = 0,
        /// <summary>
        /// Min notification importance: only shows in the shade, below the fold.
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationManager#IMPORTANCE_MIN"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Min = 1,
        /// <summary>
        /// Low notification importance: Shows in the shade, and potentially in the status bar, but is not audibly intrusive.
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationManager#IMPORTANCE_LOW"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Low = 2,
        /// <summary>
        /// Default notification importance: shows everywhere, makes noise, but does not visually intrude. 
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationManager#IMPORTANCE_DEFAULT"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Default = 3,
        /// <summary>
        /// Higher notification importance: shows everywhere, makes noise and peeks. May use full screen intents.
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationManager#IMPORTANCE_HIGH"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        High = 4,
        /// <summary>
        /// Unused.
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationManager#IMPORTANCE_MAX"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Max = 5
    }
}

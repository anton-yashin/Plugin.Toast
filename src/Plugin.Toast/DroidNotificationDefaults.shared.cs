using System;

namespace Plugin.Toast
{
    /// <summary>
    /// Default notification options for <see cref="IDroidNotificationExtension.SetDefaults(DroidNotificationDefaults)"/>
    /// </summary>
    [Flags]
    public enum DroidNotificationDefaults
    {
        /// <summary>
        /// Use all default values (where applicable).
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        All = -1,
        /// <summary>
        /// Use the default notification sound.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Sound = 1,
        /// <summary>
        /// Use the default notification vibrate.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Vibrate = 2,
        /// <summary>
        /// Use the default notification lights.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Lights = 4
    }

}

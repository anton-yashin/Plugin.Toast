using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Group alert type of android notifcation. <see cref="IDroidNotificationExtension.SetGroupAlertBehavior(DroidGroupAlert)"/>
    /// </summary>
    public enum DroidGroupAlert
    {
        /// <summary>
        /// Constant for <see cref="IDroidNotificationExtension.SetGroupAlertBehavior(DroidGroupAlert)"/>,
        /// meaning that all notifications in a group with sound or vibration ought to make sound or
        /// vibrate (respectively), so this notification will not be muted when it is in a group. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        All = 0,
        /// <summary>
        /// Constant for <see cref="IDroidNotificationExtension.SetGroupAlertBehavior(DroidGroupAlert)"/>,
        /// meaning that all children notification in a group should be silenced (no sound or vibration)
        /// even if they would otherwise make sound or vibrate. Use this constant to mute this notification
        /// if this notification is a group child. This must be applied to all children notifications you want to mute. 
        /// </summary>
        /// <remarks>
        /// For example, you might want to use this constant if you post a number of children notifications at once
        /// (say, after a periodic sync), and only need to notify the user audibly once. 
        /// <br/>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Summary = 1,
        /// <summary>
        /// Constant for <see cref="IDroidNotificationExtension.SetGroupAlertBehavior(DroidGroupAlert)"/>,
        /// meaning that the summary notification in a group should be silenced (no sound or vibration) even
        /// if they would otherwise make sound or vibrate. Use this constant to mute this notification
        /// if this notification is a group summary. 
        /// </summary>
        /// <remarks>
        /// For example, you might want to use this constant if only the children notifications in your
        /// group have content and the summary is only used to visually group notifications rather than
        /// to alert the user that new information is available. 
        /// <br/>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        Children = 2,
    }
}

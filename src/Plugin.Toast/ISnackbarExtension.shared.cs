using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// The interface of snackbar extension that availalbe on android.
    /// </summary>
    public interface ISnackbarExtension : INotificationBuilderExtension<ISnackbarExtension>
    {
        /// <summary>
        /// Set the action to be displayed in this BaseTransientBottomBar
        /// </summary>
        /// <param name="actionText">Text to display for the action</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        ISnackbarExtension WithAction(string actionText);
        /// <summary>
        /// Set the action to be displayed in this BaseTransientBottomBar
        /// </summary>
        /// <param name="actionText">Text to display for the action</param>
        /// <param name="colorResource">The text color of the action</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        ISnackbarExtension WithAction(string actionText, int colorResource);
        /// <summary>
        /// How long to display the message.
        /// </summary>
        /// <param name="duration">Duration in milliseconds.</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        ISnackbarExtension WithDuration(int duration);
        /// <summary>
        /// How long to display the message.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        ISnackbarExtension WithDuration(SnackbarDuration duration);
    }
}

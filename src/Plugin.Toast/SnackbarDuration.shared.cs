using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Predefined values for <see cref="ISnackbarExtension.WithDuration(SnackbarDuration)"/>.
    /// </summary>
    public enum SnackbarDuration
    {
        /// <summary>
        /// Show the Snackbar indefinitely. 
        /// </summary>
        Indefinite = -2,
        /// <summary>
        /// Show the Snackbar for a short period of time. 
        /// </summary>
        Short = -1,
        /// <summary>
        /// Show the Snackbar for a long period of time. 
        /// </summary>
        Long = 0
    }
}
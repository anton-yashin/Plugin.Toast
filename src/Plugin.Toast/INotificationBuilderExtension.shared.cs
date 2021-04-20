using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Base abstraction for notification builder extension classes
    /// </summary>
    /// <typeparam name="T">The type of extension</typeparam>
    public interface INotificationBuilderExtension<T> : IBuilderExtension<T>
        where T : INotificationBuilderExtension<T>
    {
        /// <summary>
        /// Add a title to notification.
        /// </summary>
        /// <param name="title">The title text</param>
        /// <returns>Instance of current builder</returns>
        T AddTitle(string title);
        /// <summary>
        /// Add a description to notification.
        /// </summary>
        /// <param name="description">The description text</param>
        /// <returns>Instance of current builder</returns>
        T AddDescription(string description);
    }
}

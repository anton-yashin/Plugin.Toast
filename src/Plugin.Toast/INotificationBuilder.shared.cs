using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Base abstraction for notification builder classes
    /// </summary>
    public interface INotificationBuilder : IBuilder
    {
        /// <summary>
        /// Add a title to notification.
        /// </summary>
        /// <param name="title">The title text.</param>
        /// <returns>Instance of current builder</returns>
        INotificationBuilder AddTitle(string title);
        /// <summary>
        /// Add a description to notification
        /// </summary>
        /// <param name="description">The description text.</param>
        /// <returns></returns>
        INotificationBuilder AddDescription(string description);
    }

    /// <summary>
    /// Base abstraction for notification builder classes
    /// </summary>
    /// <typeparam name="T">Extension type</typeparam>
    public interface INotificationBuilder<T> : INotificationBuilder
        where T : INotificationBuilderExtension<T>
    { }

    /// <summary>
    /// Base abstraction for notification builder classes
    /// </summary>
    /// <typeparam name="T1">First extension type</typeparam>
    /// <typeparam name="T2">Second extension type</typeparam>
    public interface INotificationBuilder<T1, T2> : INotificationBuilder
        where T1 : INotificationBuilderExtension<T1>
        where T2 : INotificationBuilderExtension<T2>
    { }

    /// <summary>
    /// Base abstraction for notification builder classes
    /// </summary>
    /// <typeparam name="T1">First extension type</typeparam>
    /// <typeparam name="T2">Second extension type</typeparam>
    /// <typeparam name="T3">Third extension type</typeparam>
    public interface INotificationBuilder<T1, T2, T3> : INotificationBuilder
        where T1 : INotificationBuilderExtension<T1>
        where T2 : INotificationBuilderExtension<T2>
        where T3 : INotificationBuilderExtension<T3>
    { }

    /// <summary>
    /// Base abstraction for notification builder classes
    /// </summary>
    /// <typeparam name="T1">First extension type</typeparam>
    /// <typeparam name="T2">Second extension type</typeparam>
    /// <typeparam name="T3">Third extension type</typeparam>
    /// <typeparam name="T4">Fourth extension type</typeparam>
    public interface INotificationBuilder<T1, T2, T3, T4> : IBuilder
        where T1 : INotificationBuilderExtension<T1>
        where T2 : INotificationBuilderExtension<T2>
        where T3 : INotificationBuilderExtension<T3>
        where T4 : INotificationBuilderExtension<T4>
    { }

}

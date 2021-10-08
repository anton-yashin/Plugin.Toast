using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Style for generating large-format notifications that
    /// include multiple back-and-forth messages of varying types
    /// between any number of people.
    /// In order to get a backwards compatible behavior, the app
    /// needs to use the v7 version of the notification builder
    /// together with this style, otherwise the user will see the
    /// normal notification view.
    /// Use <see cref="SetConversationTitle(string)"/> to set a
    /// conversation title for group chats with more than two people.
    /// This could be the user-created name of the group or, if it
    /// doesn't have a specific name, a list of the participants in
    /// the conversation. Do not set a conversation title for one-on-one
    /// chats, since platforms use the existence of this field as a
    /// hint that the conversation is a group. 
    /// </summary>
    /// <remarks>
    /// Portions of this page are reproduced from work created and shared by
    /// the Android Open Source Project and used according to terms described
    /// in the Creative Commons 2.5 Attribution License. 
    /// </remarks>
    public interface IMessagingStyle : IDroidStyleBuilder
    {
        /// <summary>
        /// Creates a new <see cref="AndroidX.Core.App.NotificationCompat.MessagingStyle"/>  object. 
        /// </summary>
        IMessagingStyleStep2 With(Action<IDroidPersonBuilder> personBuilder);
    }

    /// <summary>
    /// Optional fields for <see cref="IMessagingStyle"/>
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IMessagingStyleStep2
    {
        /// <summary>
        /// Adds a message for display by this notification. Use this method 
        /// for messages by the current user, in which case, the platform will insert
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.MessagingStyle#getUserDisplayName()">NotificationCompat.MessagingStyle.getUserDisplayName()</see>.
        /// </summary>
        /// <param name="text">A <see cref="string"/> to be displayed as the message content</param>
        /// <param name="timestamp">Time at which the message arrived in ms since Unix epoch</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IMessagingStyleStep2 AddMessage(string text, long timestamp);
        /// <summary>
        /// Adds a message for display by this notification. Use this method 
        /// for messages by the current user, in which case, the platform will insert
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.MessagingStyle#getUserDisplayName()">NotificationCompat.MessagingStyle.getUserDisplayName()</see>.
        /// </summary>
        /// <param name="text">A <see cref="string"/> to be displayed as the message content</param>
        /// <param name="timestamp">Time at which the message arrived in ms since Unix epoch</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IMessagingStyleStep2 AddMessage(string text, DateTime timestamp);
        /// <summary>
        /// Adds a message for display by this notification.
        /// </summary>
        /// <param name="text">A <see cref="string"/> to be displayed as the message content</param>
        /// <param name="timestamp">Time at which the message arrived in ms since Unix epoch</param>
        /// <param name="personBuilder">A Person whose <seealso cref="IDroidPersonBuilder.SetName(string)"/>
        /// value is used as the display name for the sender. You should use <seealso cref="AddMessage(string, long)"/>
        /// for messages by the current user, in which case, the platform will insert
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.MessagingStyle#getUserDisplayName()">NotificationCompat.MessagingStyle.getUserDisplayName()</see>.
        /// A Person's key should be consistent during re-posts of the notification.
        /// </param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IMessagingStyleStep2 AddMessage(string text, long timestamp, Action<IDroidPersonBuilder> personBuilder);
        /// <summary>
        /// Adds a message for display by this notification.
        /// </summary>
        /// <param name="text">A <see cref="string"/> to be displayed as the message content</param>
        /// <param name="timestamp">Time at which the message arrived in ms since Unix epoch</param>
        /// <param name="personBuilder">A Person whose <seealso cref="IDroidPersonBuilder.SetName(string)"/>
        /// value is used as the display name for the sender. You should use <seealso cref="AddMessage(string, long)"/>
        /// for messages by the current user, in which case, the platform will insert
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.MessagingStyle#getUserDisplayName()">NotificationCompat.MessagingStyle.getUserDisplayName()</see>.
        /// A Person's key should be consistent during re-posts of the notification.
        /// </param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IMessagingStyleStep2 AddMessage(string text, DateTime timestamp, Action<IDroidPersonBuilder> personBuilder);
        /// <summary>
        /// Sets the title to be displayed on this conversation. 
        /// </summary>
        /// <param name="conversationTitle">Title displayed for this conversation</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IMessagingStyleStep2 SetConversationTitle(string conversationTitle);
        /// <summary>
        /// Sets whether this conversation notification represents a group. 
        /// </summary>
        /// <param name="isGroupConversation"><b>true</b> if the conversation represents a group, <b>false</b> otherwise.</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IMessagingStyleStep2 SetGroupConversation(bool isGroupConversation);
    }

}

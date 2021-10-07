using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast.Images
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
    public interface IMessagingStyleStep2
    {
        /// <summary>
        /// Adds a message for display by this notification.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IMessagingStyleStep2 AddMessage(string text, long timestamp, Action<IDroidPersonBuilder> personBuilder);
        /// <summary>
        /// Sets the title to be displayed on this conversation. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IMessagingStyleStep2 SetConversationTitle(string conversationTitle);
        /// <summary>
        /// Sets whether this conversation notification represents a group. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IMessagingStyleStep2 SetGroupConversation(bool isGroupConversation);
    }

}

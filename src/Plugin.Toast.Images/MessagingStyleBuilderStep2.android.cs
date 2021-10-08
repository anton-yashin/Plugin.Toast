using AndroidX.Core.App;
using System;

namespace Plugin.Toast.Images
{
    sealed class MessagingStyleBuilderStep2 : IMessagingStyleStep2
    {
        readonly NotificationCompat.MessagingStyle builder;

        public MessagingStyleBuilderStep2(Person person)
        {
            builder = new NotificationCompat.MessagingStyle(person);
        }

        public IMessagingStyleStep2 AddMessage(string text, long timestamp)
        {
            builder.AddMessage(text, timestamp, (Person)null!);
            return this;
        }

        public IMessagingStyleStep2 AddMessage(string text, long timestamp, Action<IDroidPersonBuilder> personBuilder)
        {
            builder.AddMessage(text, timestamp, DroidPersonBuilder.Build(personBuilder));
            return this;
        }

        public IMessagingStyleStep2 AddMessage(string text, DateTime timestamp)
            => AddMessage(text, timestamp.ToAndroidTimeStamp());

        public IMessagingStyleStep2 AddMessage(string text, DateTime timestamp, Action<IDroidPersonBuilder> personBuilder)
            => AddMessage(text, timestamp.ToAndroidTimeStamp(), personBuilder);

        public IMessagingStyleStep2 SetConversationTitle(string conversationTitle)
        {
            builder.SetConversationTitle(conversationTitle);
            return this;
        }

        public IMessagingStyleStep2 SetGroupConversation(bool isGroupConversation)
        {
            builder.SetGroupConversation(isGroupConversation);
            return this;
        }

        public NotificationCompat.MessagingStyle Build()
            => builder;
    }
}

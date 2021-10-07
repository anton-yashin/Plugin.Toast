using AndroidX.Core.App;
using System;

namespace Plugin.Toast.Images
{
    sealed class MessagingStyleBuilder : IMessagingStyle
    {
        private MessagingStyleBuilderStep2? builder;

        public MessagingStyleBuilder() { }

        public NotificationCompat.Style Build()
        {
            if (builder == null)
                throw new InvalidOperationException(
                    "Can't build a style. You must call "
                    + nameof(IMessagingStyle)
                    + "."
                    + nameof(IMessagingStyle.With));
            return builder.Build();
        }

        public IMessagingStyleStep2 With(Action<IDroidPersonBuilder> personBuilder)
            => new MessagingStyleBuilderStep2(DroidPersonBuilder.Build(personBuilder));
    }
}

using ManualTests.ResX;
using ManualTests.Tests.Base;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManualTests.Tests
{
    sealed class MessagingStyleTest : AbstractTest<MessagingStyleTest>
    {
        private readonly IToastImageSourceFactory toastImageSourceFactory;
        private readonly IRuntimePlatform runtimePlatform;

        public MessagingStyleTest(IServiceProvider serviceProvider)
            : base(serviceProvider, Localization.R_REQUIRED_ACTION_IGNORE_NOTIFICATION, "Messaging style notification")
        {
            this.toastImageSourceFactory = serviceProvider.GetRequiredService<IToastImageSourceFactory>();
            this.runtimePlatform = serviceProvider.GetRequiredService<IRuntimePlatform>();
        }

        public override bool IsAvailable => runtimePlatform.IsAndroid;

        protected override async Task DoRunAsync()
        {
            var bli = await toastImageSourceFactory.FromResourceAsync(TestData.KEmbeddedImage, this.GetType());
            var result = await serviceProvider.GetRequiredService<INotificationBuilder>()
                .AddTitle(Localization.R_SOME_TITLE)
                .AddDescription(Localization.R_PLEASE_IGNORE)
                .WhenUsing<IDroidNotificationExtension>(
                    dne => dne.SetStyle<IMessagingStyle>(
                        bps => bps.With(pb => pb
                            .SetBot(true)
                            .SetIcon(bli)
                            .SetImportant(true)
                            .SetKey("some key 1")
                            .SetName("person name 1"))
                        .AddMessage("some text", DateTime.UtcNow - TimeSpan.FromSeconds(10), pb => pb
                            .SetBot(false)
                            .SetIcon(bli)
                            .SetImportant(false)
                            .SetKey("some key 2")
                            .SetName("person name 2"))
                        .SetConversationTitle("conversation title")
                        .SetGroupConversation(true)
                        ))
                .Build().ShowAsync();
            Assert(result == NotificationResult.Activated || result == NotificationResult.TimedOut);
        }
    }
}

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
    sealed class InboxStyleTest : AbstractTest<InboxStyleTest>
    {
        private readonly IRuntimePlatform runtimePlatform;

        public InboxStyleTest(IServiceProvider serviceProvider)
            : base(serviceProvider, Localization.R_REQUIRED_ACTION_IGNORE_NOTIFICATION, "Inbox style notification")
        {
            this.runtimePlatform = serviceProvider.GetRequiredService<IRuntimePlatform>();
        }

        public override bool IsAvailable => runtimePlatform.IsWindows;

        protected override async Task DoRunAsync()
        {
            var result = await serviceProvider.GetRequiredService<INotificationBuilder>()
                .AddTitle(Localization.R_SOME_TITLE)
                .AddDescription(Localization.R_PLEASE_IGNORE)
                .WhenUsing<IDroidNotificationExtension>(
                    dne => dne.SetStyle<IInboxStyle>(
                        @is => @is.AddLine("Line of text")
                        .AddLine("another line of text")
                        .SetBigContentTitle("Big content title")
                        .SetSummaryText("Summary text")))
                .Build().ShowAsync();
            Assert(result == NotificationResult.Activated || result == NotificationResult.TimedOut);
        }
    }
}

using ManualTests.ResX;
using ManualTests.Tests.Base;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Toast;
using System;
using System.Threading.Tasks;

namespace ManualTests.Tests
{
    sealed class BigTextStyleTest : AbstractTest<BigTextStyleTest>
    {
        private readonly IRuntimePlatform runtimePlatform;

        public BigTextStyleTest(IServiceProvider serviceProvider)
            : base(serviceProvider, Localization.R_REQUIRED_ACTION_IGNORE_NOTIFICATION, "Big text style notification")
        {
            this.runtimePlatform = serviceProvider.GetRequiredService<IRuntimePlatform>();
        }

        public override bool IsAvailable => runtimePlatform.IsAndroid;

        protected override async Task DoRunAsync()
        {
            var result = await serviceProvider.GetRequiredService<INotificationBuilder>()
                .AddTitle(Localization.R_SOME_TITLE)
                .AddDescription(Localization.R_PLEASE_IGNORE)
                .WhenUsing<IDroidNotificationExtension>(
                    dne => dne.SetStyle<IBigTextStyle>(
                        bts => bts.BigText("Big Text")
                        .SetBigContentTitle("Big Content Title")
                        .SetSummaryText("Summary Text")))
                .Build().ShowAsync();
            Assert(result == NotificationResult.Activated || result == NotificationResult.TimedOut);
        }
    }
}

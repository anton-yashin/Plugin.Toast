using ManualTests.ResX;
using ManualTests.Tests.Base;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ManualTests.Tests
{
    sealed class BigTextStyleTest : AbstractTest<BigTextStyleTest>
    {
        public BigTextStyleTest(IServiceProvider serviceProvider)
            : base(serviceProvider, Localization.R_REQUIRED_ACTION_IGNORE_NOTIFICATION, "Big text style notification")
        {
        }

        public override bool IsAvailable => Device.RuntimePlatform == Device.Android;

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

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Plugin.Toast;
using ManualTests.ResX;
using ManualTests.Tests.Base;
using Plugin.Toast.Abstractions;

namespace ManualTests
{
    sealed class ShowStandardTest : AbstractTest<ShowStandardTest>
    {
        public ShowStandardTest(IServiceProvider serviceProvider)
            : base(serviceProvider, Localization.R_REQUIRED_ACTION_ACTIVATE_NOTIFICATION, Localization.R_TEST_NAME_SHOW_STANDARD)
        {
        }

        protected override async Task DoRunAsync()
        {
            var result = await serviceProvider.GetRequiredService<INotificationBuilder>()
                .AddTitle(Localization.R_SOME_TITLE).AddDescription(Localization.R_TAP_ME)
                .Build().ShowAsync();
            Assert(result == NotificationResult.Activated);
        }
    }
}

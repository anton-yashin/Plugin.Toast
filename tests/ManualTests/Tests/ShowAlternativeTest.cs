using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Plugin.Toast;
using ManualTests.ResX;
using ManualTests.Tests.Base;
using Plugin.Toast.Abstractions;

namespace ManualTests.Tests
{
    sealed class ShowAlternativeTest : AbstractTest<ShowAlternativeTest>
    {
        private readonly IRuntimePlatform runtimePlatform;

        public ShowAlternativeTest(IServiceProvider serviceProvider)
            : base (serviceProvider, Localization.R_REQUIRED_ACTION_ACTIVATE_NOTIFICATION, Localization.R_TEST_NAME_SHOW_ALTERNATIVE)
        {
            this.runtimePlatform = serviceProvider.GetRequiredService<IRuntimePlatform>();
        }

        public override bool IsAvailable => runtimePlatform.IsWindows == false;

        protected override async Task DoRunAsync()
        {
            var result = await serviceProvider.GetRequiredService<INotificationBuilder<ISnackbarExtension, IIosLocalNotificationExtension>>()
                .AddTitle(Localization.R_SOME_TITLE).AddDescription(Localization.R_LOREM_IPSUM)
                .WhenUsing<ISnackbarExtension>(_ => _.WithAction(Localization.R_TAP_ME))
                .Build().ShowAsync();
            var expected = runtimePlatform.IsIos ? NotificationResult.Unknown : NotificationResult.Activated;
            Assert(result == expected);
        }
    }
}

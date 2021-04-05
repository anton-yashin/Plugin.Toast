using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Toast;
using ManualTests.ResX;
using ManualTests.Tests.Base;

namespace ManualTests.Tests
{
    sealed class ShowAlternativeTest : AbstractTest<ShowAlternativeTest>
    {
        public ShowAlternativeTest(IServiceProvider serviceProvider)
            : base (serviceProvider, Localization.R_REQUIRED_ACTION_ACTIVATE_NOTIFICATION, Localization.R_TEST_NAME_SHOW_ALTERNATIVE)
        { }

        public override bool IsAvailable => Device.RuntimePlatform != Device.UWP;

        protected override async Task DoRunAsync()
        {
            var result = await serviceProvider.GetRequiredService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension>>()
                .AddTitle(Localization.R_SOME_TITLE).AddDescription(Localization.R_LOREM_IPSUM)
                .WhenUsing<ISnackbarExtension>(_ => _.WithAction(Localization.R_TAP_ME))
                .Build().ShowAsync();
            var expected = Device.RuntimePlatform == Device.iOS ? NotificationResult.Unknown : NotificationResult.Activated;
            Assert(result == expected);
        }
    }
}

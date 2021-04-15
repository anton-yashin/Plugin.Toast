using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Toast;
using ManualTests.ResX;
using ManualTests.Tests.Base;
using Plugin.Toast.Abstractions;

namespace ManualTests.Tests
{
    sealed class IgnoreAlternative : AbstractTest<IgnoreStandard>
    {
        public IgnoreAlternative(IServiceProvider serviceProvider)
            : base(serviceProvider, Localization.R_REQUIRED_ACTION_IGNORE_NOTIFICATION, Localization.R_TEST_NAME_IGNORE_ALTERNATIVE)
        { }

        public override bool IsAvailable => Device.RuntimePlatform != Device.UWP;

        protected override async Task DoRunAsync()
        {
            var result = await serviceProvider.GetRequiredService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension>>()
                .AddTitle(Localization.R_SOME_TITLE).AddDescription(Localization.R_LOREM_IPSUM)
                .WhenUsing<ISnackbarExtension>(_ => _.WithAction(Localization.R_DONT_TAP))
                .Build().ShowAsync();
            var expected = Device.RuntimePlatform == Device.iOS ? NotificationResult.Unknown : NotificationResult.TimedOut;
            Assert(result == expected);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Plugin.Toast;
using ManualTests.ResX;
using ManualTests.Tests.Base;
using Plugin.Toast.Abstractions;

namespace ManualTests.Tests
{
    sealed class IgnoreStandard : AbstractTest<IgnoreStandard>
    {
        public IgnoreStandard(IServiceProvider serviceProvider)
            :base (serviceProvider, Localization.R_REQUIRED_ACTION_IGNORE_NOTIFICATION, Localization.R_TEST_NAME_IGNORE_STANDARD)
        { }

        protected override async Task DoRunAsync()
        {
            var result = await serviceProvider.GetRequiredService<IBuilder>()
                .AddTitle(Localization.R_SOME_TITLE).AddDescription(Localization.R_PLEASE_IGNORE)
                .Build().ShowAsync();
            Assert(result == NotificationResult.TimedOut);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Plugin.Toast;
using ManualTests.ResX;
using ManualTests.Tests.Base;
using Plugin.Toast.Abstractions;

namespace ManualTests.Tests
{
    sealed class ShowScheduled : AbstractTest<ShowScheduled>
    {
        public ShowScheduled(IServiceProvider serviceProvider)
            :base (serviceProvider, Localization.R_REQUIRED_ACTION_IGNORE_NOTIFICATION, Localization.R_TEST_NAME_SHOW_SCHEDULED)
        { }

        protected override async Task DoRunAsync()
        {
            await serviceProvider.GetRequiredService<IInitialization>().InitializeAsync();
            var scheduleTo = DateTimeOffset.Now + TimeSpan.FromSeconds(3);
            var builder = serviceProvider.GetRequiredService<INotificationBuilder>();
            using var token = builder.AddTitle(Localization.R_SOME_TITLE).AddDescription(Localization.R_PLEASE_IGNORE)
                .Build().ScheduleTo(scheduleTo);
            await Task.Delay(TimeSpan.FromSeconds(6));
            Assert(true);
        }
    }
}

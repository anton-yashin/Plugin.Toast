using Microsoft.Extensions.DependencyInjection;
using ManualTests.ResX;
using ManualTests.Tests.Base;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Toast.Abstractions;

namespace ManualTests.Tests
{
    sealed class CancelScheduled : AbstractTest<CancelScheduled>
    {
        public CancelScheduled(IServiceProvider serviceProvider)
            : base(serviceProvider, Localization.R_REQUIRED_ACTION_IGNORE_NOTIFICATION, Localization.R_DESCRIPTION_CANCEL_SCHEDULED)
        { }

        protected override async Task DoRunAsync()
        {
            await serviceProvider.GetRequiredService<IInitialization>().InitializeAsync();
            var scheduleTo = DateTimeOffset.Now + TimeSpan.FromSeconds(4);
            var builder = serviceProvider.GetRequiredService<INotificationBuilder>();
            var token = builder.AddTitle(Localization.R_SOME_TITLE).AddDescription("Test failed if you see this")
                .Build().ScheduleTo(scheduleTo);
            await Task.Delay(TimeSpan.FromSeconds(1));
            token.Dispose();
            await Task.Delay(TimeSpan.FromSeconds(4));
            Assert(true);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Plugin.Toast;
using ManualTests.ResX;
using ManualTests.Tests.Base;

namespace ManualTests.Tests
{
    sealed class ActivateFromNotificationCenter : AbstractTest<IgnoreStandard>
    {
        private readonly INotificationEventSource eventSender;
        ToastId? tid;

        public ActivateFromNotificationCenter(IServiceProvider serviceProvider)
            :base (serviceProvider, "Wait when hidded and activate from notification center", "Activation of not active notification")
        {
            this.eventSender = serviceProvider.GetRequiredService<INotificationEventSource>();
        }

        private void OnNotificationEvent(object sender, NotificationEvent e)
        {
            Assert(e.ToastId == tid);
            eventSender.NotificationReceived -= OnNotificationEvent;
        }

        protected override async Task DoRunAsync()
        {
            eventSender.NotificationReceived += OnNotificationEvent;
            var result = await serviceProvider.GetService<IBuilder>()
                .AddTitle(Localization.R_SOME_TITLE).AddDescription(Localization.R_PLEASE_IGNORE)
                .Build().ShowAsync(out tid);
            if (result == NotificationResult.Activated)
                Assert(false);
        }
    }
}

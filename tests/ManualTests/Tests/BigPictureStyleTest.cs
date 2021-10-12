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
    sealed class BigPictureStyleTest : AbstractTest<BigPictureStyleTest>
    {
        private readonly IToastImageSourceFactory toastImageSourceFactory;

        public BigPictureStyleTest(IServiceProvider serviceProvider)
            : base(serviceProvider, Localization.R_REQUIRED_ACTION_IGNORE_NOTIFICATION, "Big picture style notification")
        {
            this.toastImageSourceFactory = serviceProvider.GetRequiredService<IToastImageSourceFactory>();
        }

        public override bool IsAvailable => Device.RuntimePlatform == Device.Android;

        protected override async Task DoRunAsync()
        {
            var result = await serviceProvider.GetRequiredService<INotificationBuilder>()
                .AddTitle(Localization.R_SOME_TITLE)
                .AddDescription(Localization.R_PLEASE_IGNORE)
                .WhenUsingAsync<IDroidNotificationExtension>(
                    dne => dne.SetStyleAsync<IBigPictureStyle>(
                        async bps => bps.BigLargeIcon(await toastImageSourceFactory.FromResourceAsync(TestData.KEmbeddedImage, this.GetType()))
                        .BigPicture(await toastImageSourceFactory.FromResourceAsync(TestData.KEmbeddedImage, this.GetType()))
                        .SetBigContentTitle("Big content title")
                        .SetSummaryText("Summary text")))
                .BuildAsync().ShowAsync();
            Assert(result == NotificationResult.Activated || result == NotificationResult.TimedOut);
        }
    }
}

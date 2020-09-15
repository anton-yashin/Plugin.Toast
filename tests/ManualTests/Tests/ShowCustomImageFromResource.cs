using ManualTests.ResX;
using ManualTests.Tests.Base;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManualTests.Tests
{
    sealed class ShowCustomImageFromResource : AbstractTest<ShowCustomImageFromUri>
    {
        public ShowCustomImageFromResource(IServiceProvider serviceProvider)
            : base(serviceProvider, Localization.R_REQUIRED_ACTION_IGNORE_NOTIFICATION, "Show notification with custom image from embedded resource")
        { }

        protected override async Task DoRunAsync()
        {
            var result = await serviceProvider.GetService<IBuilder>()
                       .AddTitle(Localization.R_SOME_TITLE)
                       .AddDescription(Localization.R_LOREM_IPSUM)
                       .AddImage(await ToastImageSource.FromResourceAsync(TestData.KEmbeddedImage))
                       .Build().ShowAsync();
            Assert(result == NotificationResult.Activated || result == NotificationResult.TimedOut);
        }
    }
}

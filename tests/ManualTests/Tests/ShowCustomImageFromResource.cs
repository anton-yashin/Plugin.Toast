using ManualTests.ResX;
using ManualTests.Tests.Base;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace ManualTests.Tests
{
    sealed class ShowCustomImageFromResource : AbstractTest<ShowCustomImageFromUri>
    {
        private readonly IToastImageSourceFactory toastImageSourceFactory;

        public ShowCustomImageFromResource(IServiceProvider serviceProvider)
            : base(serviceProvider, Localization.R_REQUIRED_ACTION_IGNORE_NOTIFICATION, Localization.R_DESCIRPTION_FROM_RESOURCE)
        {
            this.toastImageSourceFactory = serviceProvider.GetRequiredService<IToastImageSourceFactory>();
        }

        protected override async Task DoRunAsync()
        {
            var result = await serviceProvider.GetRequiredService<IBuilder>()
                       .AddTitle(Localization.R_SOME_TITLE)
                       .AddDescription(Localization.R_LOREM_IPSUM)
                       .AddImage(await toastImageSourceFactory.FromResourceAsync(TestData.KEmbeddedImage, this.GetType())) 
                       .Build().ShowAsync();
            Assert(result == NotificationResult.Activated || result == NotificationResult.TimedOut);
        }
    }
}

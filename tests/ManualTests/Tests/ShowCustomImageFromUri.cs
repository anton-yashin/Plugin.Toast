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
    sealed class ShowCustomImageFromUri : AbstractTest<ShowCustomImageFromUri>
    {
        private readonly IToastImageSourceFactory toastImageSourceFactory;

        public ShowCustomImageFromUri(IServiceProvider serviceProvider)
            : base(serviceProvider, Localization.R_REQUIRED_ACTION_IGNORE_NOTIFICATION, Localization.R_DESCRIPTION_FROM_URI)
        {
            this.toastImageSourceFactory = serviceProvider.GetRequiredService<IToastImageSourceFactory>();
        }

        protected override async Task DoRunAsync()
        {
            var result = await serviceProvider.GetRequiredService<IBuilder>()
                       .AddTitle(Localization.R_SOME_TITLE)
                       .AddDescription(Localization.R_LOREM_IPSUM)
                       .AddImage(await toastImageSourceFactory.FromUriAsync(new Uri("https://picsum.photos/200?randomData=" + Guid.NewGuid().ToString(), UriKind.Absolute)))
                       .Build().ShowAsync();
            Assert(result == NotificationResult.Activated || result == NotificationResult.TimedOut);
        }
    }
}

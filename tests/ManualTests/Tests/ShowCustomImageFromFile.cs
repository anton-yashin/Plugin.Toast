﻿using ManualTests.ResX;
using ManualTests.Tests.Base;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ManualTests.Tests
{
    sealed class ShowCustomImageFromFile : AbstractTest<ShowCustomImageFromUri>
    {
        private readonly IToastImageSourceFactory toastImageSourceFactory;

        public ShowCustomImageFromFile(IServiceProvider serviceProvider)
            : base(serviceProvider, Localization.R_REQUIRED_ACTION_IGNORE_NOTIFICATION, "Show notification with custom image from file")
        {
            this.toastImageSourceFactory = serviceProvider.GetRequiredService<IToastImageSourceFactory>();
        }

        protected override async Task DoRunAsync()
        {
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "file.png");
            var src = Assembly.GetExecutingAssembly().GetManifestResourceStream(TestData.KEmbeddedImage);
            using (var file = File.OpenWrite(fileName))
            {
                await src.CopyToAsync(file);
                await file.FlushAsync();
            }

            var result = await serviceProvider.GetService<IBuilder>()
                        .AddTitle(Localization.R_SOME_TITLE)
                        .AddDescription(Localization.R_LOREM_IPSUM)
                        .AddImage(await toastImageSourceFactory.FromFileAsync(fileName))
                        .Build().ShowAsync();
            Assert(result == NotificationResult.Activated || result == NotificationResult.TimedOut);
        }
    }
}
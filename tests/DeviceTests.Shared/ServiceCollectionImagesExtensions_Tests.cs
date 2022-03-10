#if NETCORE_APP == false
#nullable enable

using Microsoft.Extensions.DependencyInjection;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
#if __ANDROID__
using Plugin.Toast.Droid.Configuration;
#if NET6_0_OR_GREATER
using Microsoft.Maui.Platform;
#else
using Xamarin.Forms.Platform.Android;
#endif
#endif
#if WINDOWS
#if NET6_0_OR_GREATER
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
#else
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
#endif
#endif
using Xunit;

namespace DeviceTests
{
    public class ServiceCollectionImagesExtensions_Tests
    {
#if __ANDROID__
        [Fact]
        public void AddNotificationManagerImagesSupport()
        {
            // prepare & act
            var sc = new ServiceCollection();
            sc.AddNotificationManager(b => b.WithActivity(Platform.Activity));
            sc.AddNotificationManagerImagesSupport();
            using var sp = sc.BuildServiceProvider();

            // verify
            Assert.NotNull(sp.GetService<IExtensionPlugin<Plugin.Toast.Droid.IPlatformSpecificExtension, ToastImageSource, Router.Route>>());
            Assert.NotNull(sp.GetService<IImageCacher>());
            Assert.NotNull(sp.GetService<IToastImageSourceFactory>());
            Assert.NotNull(sp.GetService<IHttpClientFactory>());
            Assert.NotNull(sp.GetService<IBigPictureStyle>());
            Assert.NotNull(sp.GetService<IMessagingStyle>());
        }
#elif __IOS__
        [Fact]
        public void AddNotificationManagerImagesSupport()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
        {
            // prepare & act
            var sc = new ServiceCollection();
            sc.AddNotificationManagerImagesSupport();
            using var sp = sc.BuildServiceProvider();

            // verify
            Assert.NotNull(sp.GetService<IOsImageRouter>());
            Assert.NotNull(sp.GetService<IExtensionPlugin<Plugin.Toast.IOS.IPlatformSpecificExtension, ToastImageSource, Router.Route>>());
            Assert.NotNull(sp.GetService<IExtensionPlugin<Plugin.Toast.IOS.IPlatformSpecificExtension, IEnumerable<ToastImageSource>>>());
            Assert.NotNull(sp.GetService<IImageCacher>());
            Assert.NotNull(sp.GetService<IToastImageSourceFactory>());
            Assert.NotNull(sp.GetService<IUriToFileNameStrategy>());
            Assert.NotNull(sp.GetService<IBundleToFileNameStrategy>());
            Assert.NotNull(sp.GetService<IResourceToFileNameStrategy>());
            Assert.NotNull(sp.GetService<IMimeDetector>());
            Assert.NotNull(sp.GetService<IHttpClientFactory>());
        });
#elif WINDOWS
        [Fact]
        public void AddNotificationManagerImagesSupport()
        {
            // prepare & act
            var sc = new ServiceCollection();
            sc.AddNotificationManagerImagesSupport(
#if NET6_0_OR_GREATER
                Microsoft.Maui.Controls.Application.Current.OnThisPlatform().GetImageDirectory
#else
                Xamarin.Forms.Application.Current.OnThisPlatform().GetImageDirectory
#endif
                );
            using var sp = sc.BuildServiceProvider();

            // verify
            Assert.NotNull(sp.GetService<IExtensionPlugin<Plugin.Toast.UWP.IPlatformSpecificExtension, ToastImageSource, Router.Route>>());
            Assert.NotNull(sp.GetService<IImageCacher>());
            Assert.NotNull(sp.GetService<IToastImageSourceFactory>());
            Assert.NotNull(sp.GetService<IResourceToFileNameStrategy>());
        }
#endif
        }
}

#endif
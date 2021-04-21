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
using Xamarin.Forms.Platform.Android;
#endif
#if NETFX_CORE
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
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
            sc.AddNotificationManagerImagesSupport(fn => global::Android.App.Application.Context.Resources.GetBitmapAsync(fn));
            using var sp = sc.BuildServiceProvider();

            // verify
            Assert.NotNull(sp.GetService<IExtensionPlugin<Plugin.Toast.Droid.IPlatformSpecificExtension, ToastImageSource, Router.Route>>());
            Assert.NotNull(sp.GetService<IImageCacher>());
            Assert.NotNull(sp.GetService<IToastImageSourceFactory>());
            Assert.NotNull(sp.GetService<IHttpClientFactory>());
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
#elif NETFX_CORE
        [Fact]
        public void AddNotificationManagerImagesSupport()
        {
            // prepare & act
            var sc = new ServiceCollection();
            sc.AddNotificationManagerImagesSupport(() => Xamarin.Forms.Application.Current?.OnThisPlatform().GetImageDirectory());
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
using Microsoft.Extensions.DependencyInjection;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using UnitTests.Mocks;
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using Xunit;

namespace DeviceTests.UWP
{
    public class ToastImageSourceFactory_Tests
    {

        [Fact]
        public async Task FromUriAsync()
        {
            // prepare
            var expectedUri = new Uri("https://expected.com", UriKind.Absolute);
            using var sp = CreateServices();
            var factory = sp.GetService<ToastImageSourceFactory>();

            // act
            var result = await factory.FromUriAsync(expectedUri);

            // verify
            Assert.Same(expectedUri, result.ImageUri);
        }

        [Theory, MemberData(nameof(FormFileAsyncData))]
        public async Task FromFileAsync(string expected, string arg, string imageDir)
        {
            // prepare
            using var sp = CreateServices();
            var factory = sp.GetService<ToastImageSourceFactory>();
            Xamarin.Forms.Application.Current?.OnThisPlatform().SetImageDirectory(imageDir);

            // act
            var result = await factory.FromFileAsync(arg);

            // assert
            Assert.Equal(expected, result.ImageUri.ToString());
        }

        public static IEnumerable<object[]> FormFileAsyncData()
        {
            yield return new object[] { "file:///c:/image.png", "c:\\image.png", "" };
            yield return new object[] { "file:///" + Path.GetFullPath("image.png").Replace('\\', '/'), "image.png", "" };
            yield return new object[] { "file:///c:/image.png", "c:\\image.png", "ImageDir" };
            yield return new object[] { "file:///" + Path.GetFullPath("ImageDir\\image.png").Replace('\\', '/'), "image.png", "ImageDir" };
        }

        [Fact]
        public async Task FromResourceAsync()
        {
            // prepare
            const string KFullPath = "c:\\fullpath\\image.png";
            const string KArg = "image.png";
            const string KLocalPath = "localpath\\image.png";
            const string KExepcted = "file:///c:/fullpath/image.png";
            var expectedAssembly = Assembly.GetExecutingAssembly();
            using var sp = CreateServices();
            var mic = (MockImageCacher)sp.GetService<IImageCacher>();
            var strategy = (MockResourceToFileNameStrategy)sp.GetService<IResourceToFileNameStrategy>();
            var factory = sp.GetService<ToastImageSourceFactory>();

            strategy.OnConvert = (rp, assembly) =>
            {
                Assert.Equal(KArg, rp);
                Assert.Same(expectedAssembly, assembly);
                return KLocalPath;
            };

            mic.OnCacheAsync = (localPath, ct, copyToAsync) =>
            {
                Assert.Equal(KLocalPath, localPath);
                return Task.FromResult(KFullPath);
            };

            // act
            var result = await factory.FromResourceAsync(KArg, expectedAssembly);

            // verify
            Assert.Equal(KExepcted, result.ImageUri.ToString());
        }

        static ServiceProvider CreateServices()
        {
            var sc = new ServiceCollection();
            sc.AddSingleton<IImageCacher, MockImageCacher>();
            sc.AddSingleton<IResourceToFileNameStrategy, MockResourceToFileNameStrategy>();
            sc.AddSingleton<ToastImageSourceFactory>();
            return sc.BuildServiceProvider();
        }
    }
}

#nullable enable

using Microsoft.Extensions.DependencyInjection;
using LightMock;
using LightMock.Generator;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using UnitTests;
using Xunit;
using System.Threading;
using Plugin.Toast.Images;
#if NET6_0_OR_GREATER
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
#else
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
#endif

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
            var factory = sp.GetRequiredService<ToastImageSourceFactory>();

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
            var factory = sp.GetRequiredService<ToastImageSourceFactory>();
#if NET6_0_OR_GREATER
            Microsoft.Maui.Controls.Application.Current.OnThisPlatform().SetImageDirectory(imageDir);
#else
            Xamarin.Forms.Application.Current?.OnThisPlatform().SetImageDirectory(imageDir);
#endif

            // act
            var result = await factory.FromFileAsync(arg);

            // assert
            Assert.Equal(expected, result.ImageUri.ToString());
        }

        public static IEnumerable<object[]> FormFileAsyncData()
        {
            yield return new object[] { "file:///c:/image.png", "c:\\image.png", "" };
            yield return new object[] { "file:///" + GetFullPath("image.png").Replace('\\', '/'), "image.png", "" };
            yield return new object[] { "file:///c:/image.png", "c:\\image.png", "ImageDir" };
            yield return new object[] { "file:///" + GetFullPath("ImageDir\\image.png").Replace('\\', '/'), "image.png", "ImageDir" };
        }

        static string GetFullPath(string path)
        {
#if NET6_0_OR_GREATER
            return Path.Combine(PackageInfo.GetRoot(), path);
#else
            return Path.GetFullPath(path);
#endif
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
            var mic = sp.GetRequiredService<Mock<IImageCacher>>();
            var strategy = sp.GetRequiredService<Mock<IResourceToFileNameStrategy>>();
            var factory = sp.GetRequiredService<ToastImageSourceFactory>();

            strategy.Arrange(f => f.Convert(The<string>.IsAnyValue, The<Assembly>.IsAnyValue))
                .Returns(() => KLocalPath);
            mic.Arrange(f => f.CacheAsync(The<string>.IsAnyValue, The<CancellationToken>.IsAnyValue, The<Func<Stream, CancellationToken, Task>>.IsAnyValue))
                .ReturnsAsync(() => KFullPath);

            // act
            var result = await factory.FromResourceAsync(KArg, expectedAssembly);

            // verify
            Assert.Equal(KExepcted, result.ImageUri.ToString());
            strategy.Assert(f => f.Convert(
                The<string>.Is(rp => KArg == rp),
                The<Assembly>.Is(assembly => ReferenceEquals(expectedAssembly, assembly))));
            mic.Assert(f => f.CacheAsync(The<string>.Is(lp => KLocalPath == lp), 
                The<CancellationToken>.IsAnyValue, The<Func<Stream, CancellationToken, Task>>.IsAnyValue));
        }

        static ServiceProvider CreateServices()
        {
            var sc = new ServiceCollection();
            sc.AddMock<IImageCacher, IResourceToFileNameStrategy>();
            sc.AddSingleton<IImageDirectoryResolver>(sc => new ImageDirectoryResolver(
#if NET6_0_OR_GREATER
                () => Microsoft.Maui.Controls.Application.Current.OnThisPlatform().GetImageDirectory()
#else
                () => Xamarin.Forms.Application.Current?.OnThisPlatform().GetImageDirectory()
#endif
            ));

            sc.AddSingleton<ToastImageSourceFactory>();
            return sc.BuildServiceProvider();
        }
    }
}

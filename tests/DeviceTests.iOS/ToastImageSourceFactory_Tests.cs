#nullable enable

using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using LightMock;
using LightMock.Generator;
using Plugin.Toast;
using UnitTests;
using UnitTests.Mocks;
using Xunit;
using System.Threading;

namespace DeviceTests.iOS
{
    public class ToastImageSourceFactory_Tests : IClassFixture<ToastImageSourceFactory_Tests.Fixture>
    {
        const string KResource = "DeviceTests.iOS.Images.embedded_image.jpg";
        const string KFile = "ToastImageSourceFactory_Tests.png";

        [Fact]
        public async Task FromUriAsync()
        {
            // arrange
            const string KStrategyResult = "network/image.png";
            const string KMime = "image/png";
            var expectedUri = new Uri("https://example.com", UriKind.Absolute);
            using var sp = CreateServices();
            var mockCacher = sp.GetRequiredService<Mock<IImageCacher>>();
            mockCacher.Arrange(f => f.CacheAsync(The<string>.IsAnyValue, The<CancellationToken>.IsAnyValue, The<Func<Stream, CancellationToken, Task>>.IsAnyValue))
                .ReturnsAsync(() => FullFileName);
            var mockStrategy = sp.GetRequiredService<Mock<IUriToFileNameStrategy>>();
            mockStrategy.Arrange(f => f.Convert(The<Uri>.IsAnyValue))
                .Returns(() => KStrategyResult);
            var mockMimeDetector = sp.GetRequiredService<Mock<IMimeDetector>>();
            mockMimeDetector.Arrange(f => f.DetectAsync(The<Stream>.IsAnyValue, The<CancellationToken>.IsAnyValue))
                .ReturnsAsync(() => KMime);
            var factory = sp.GetRequiredService<ToastImageSourceFactory>();

            // act
            var result = await factory.FromUriAsync(expectedUri);

            // assert
            Assert.NotNull(result);
            Assert.NotNull(result.Attachment);
            mockCacher.Assert(f => f.CacheAsync(The<string>.Is(path => KStrategyResult == path),
                The<CancellationToken>.IsAnyValue, The<Func<Stream, CancellationToken, Task>>.IsAnyValue));
            mockStrategy.Assert(f => f.Convert(The<Uri>.Is(uri => uri == expectedUri)));
            mockMimeDetector.Assert(f => f.DetectAsync(The<Stream>.Is(s => s != null), The<CancellationToken>.IsAnyValue));
        }

        [Fact]
        public async Task FromResourceAsync()
        {
            // arrange
            const string KStrategyResult = "resources/image.png";
            var expectedAssembly = Assembly.GetExecutingAssembly();
            using var sp = CreateServices();
            var mockCacher = sp.GetRequiredService<Mock<IImageCacher>>();
            mockCacher.Arrange(f => f.CacheAsync(The<string>.IsAnyValue, The<CancellationToken>.IsAnyValue, The<Func<Stream, CancellationToken, Task>>.IsAnyValue))
                .ReturnsAsync(() => FullFileName);
            var mockStrategy = sp.GetRequiredService<Mock<IResourceToFileNameStrategy>>();
            mockStrategy.Arrange(f => f.Convert(The<string>.IsAnyValue, The<Assembly>.IsAnyValue))
                .Returns(() => KStrategyResult);
            var factory = sp.GetRequiredService<ToastImageSourceFactory>();

            // act
            var result = await factory.FromResourceAsync(KResource, this.GetType());

            // verify
            Assert.NotNull(result);
            Assert.NotNull(result.Attachment);
            mockCacher.Assert(f => f.CacheAsync(The<string>.Is(rp => KStrategyResult == rp),
                The<CancellationToken>.IsAnyValue, The<Func<Stream, CancellationToken, Task>>.IsAnyValue));
            mockStrategy.Assert(f => f.Convert(The<string>.Is(rp => KResource == rp),
                The<Assembly>.Is(a => a == expectedAssembly)));
        }

        [Fact]
        public async Task FromFileAsync()
        {
            // prepare
            using var sp = CreateServices();
            var factory = sp.GetRequiredService<ToastImageSourceFactory>();

            // act
            var result = await factory.FromFileAsync(FullFileName);

            // verify
            Assert.NotNull(result);
            Assert.NotNull(result.Attachment);
        }

        static ServiceProvider CreateServices()
        {
            var sc = new ServiceCollection();
            sc.AddMock<IHttpClientFactory, IImageCacher, IUriToFileNameStrategy, IResourceToFileNameStrategy>();
            sc.AddMock<IBundleToFileNameStrategy, IMimeDetector>();
            sc.AddSingleton<ToastImageSourceFactory>();
            return sc.BuildServiceProvider();
        }

        static string FullFileName => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), KFile);

        public sealed class Fixture : IDisposable
        {
            public Fixture()
            {
                using (var file = File.Create(FullFileName))
                using (var src = Assembly.GetExecutingAssembly().GetManifestResourceStream(KResource))
                {
                    src.CopyTo(file);
                    file.Flush();
                }
            }

            public void Dispose()
            {
                File.Delete(FullFileName);
            }
        }
    }
}
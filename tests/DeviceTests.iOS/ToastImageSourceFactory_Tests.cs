using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Toast;
using UIKit;
using UnitTests.Mocks;
using Xunit;

namespace DeviceTests.iOS
{
    public class ToastImageSourceFactory_Tests : IClassFixture<ToastImageSourceFactory_Tests.Fixture>
    {
        const string KResource = "DeviceTests.iOS.Images.embedded_image.jpg";
        const string KFile = "ToastImageSourceFactory_Tests.png";

        [Fact]
        public async Task FromUriAsync()
        {
            // prepare
            const string KStrategyResult = "network/image.png";
            const string KMime = "image/png";
            var expectedUri = new Uri("https://example.com", UriKind.Absolute);
            using var sp = CreateServices();
            var mockCacher = (MockImageCacher)sp.GetService<IImageCacher>();
            mockCacher.OnCacheAsync = (rp, ct, copyToAsync) =>
            {
                Assert.Equal(KStrategyResult, rp);
                return Task.FromResult(FullFileName);
            };
            var mockStrategy = (MockUriToFileNameStrategy)sp.GetService<IUriToFileNameStrategy>();
            mockStrategy.OnConvert = uri =>
            {
                Assert.Equal(expectedUri, uri);
                return KStrategyResult;
            };
            var mockMimeDetector = (MockMimeDetector)sp.GetService<IMimeDetector>();
            mockMimeDetector.OnDetectAsync = (s, ct) =>
            {
                Assert.NotNull(s);
                return Task.FromResult(KMime);
            };
            var factory = sp.GetService<ToastImageSourceFactory>();

            // act
            var result = await factory.FromUriAsync(expectedUri);

            // verify
            Assert.NotNull(result);
            Assert.NotNull(result.Attachment);
        }

        [Fact]
        public async Task FromResourceAsync()
        {
            // prepare
            const string KStrategyResult = "resources/image.png";
            var expectedAssembly = Assembly.GetExecutingAssembly();
            using var sp = CreateServices();
            var mockCacher = (MockImageCacher)sp.GetService<IImageCacher>();
            mockCacher.OnCacheAsync = (rp, ct, copyToAsync) =>
            {
                Assert.Equal(KStrategyResult, rp);
                return Task.FromResult(FullFileName);
            };
            var mockStrategy = (MockResourceToFileNameStrategy)sp.GetService<IResourceToFileNameStrategy>();
            mockStrategy.OnConvert = (rp, a) =>
            {
                Assert.Equal(expectedAssembly, a);
                Assert.Equal(KResource, rp);
                return KStrategyResult;
            };
            var factory = sp.GetService<ToastImageSourceFactory>();

            // act
            var result = await factory.FromResourceAsync(KResource, this.GetType());

            // verify
            Assert.NotNull(result);
            Assert.NotNull(result.Attachment);
        }

        [Fact]
        public async Task FromFileAsync()
        {
            // prepare
            using var sp = CreateServices();
            var factory = sp.GetService<ToastImageSourceFactory>();

            // act
            var result = await factory.FromFileAsync(FullFileName);

            // verify
            Assert.NotNull(result);
            Assert.NotNull(result.Attachment);
        }

        static ServiceProvider CreateServices()
        {
            var sc = new ServiceCollection();
            sc.AddSingleton<IHttpClientFactory, MockHttpClientFactory>();
            sc.AddSingleton<IImageCacher, MockImageCacher>();
            sc.AddSingleton<IUriToFileNameStrategy, MockUriToFileNameStrategy>();
            sc.AddSingleton<IResourceToFileNameStrategy, MockResourceToFileNameStrategy>();
            sc.AddSingleton<IBundleToFileNameStrategy, MockBundleToFileNameStrategy>();
            sc.AddSingleton<IMimeDetector, MockMimeDetector>();
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
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Toast;
using UnitTests.Mocks;
using Xunit;

namespace DeviceTests.Android
{
    public class ToastImageSourceFactory_Tests
    {
        const string KResource = "DeviceTests.Android.Images.embedded_image.jpg";

        [Fact]
        public async Task FromResourceAsync()
        {
            // prepare
            using var sp = CreateServices();
            var factory = sp.GetService<ToastImageSourceFactory>();

            // act
            var result = await factory.FromResourceAsync(KResource);

            // verify
            Assert.NotNull(result);
            Assert.NotNull(result.Bitmap);
        }

        [Fact]
        public async Task FromBundleAsync()
        {
            // prepare
            using var sp = CreateServices();
            var factory = sp.GetService<ToastImageSourceFactory>();

            // act
            var result = await factory.FromFileAsync("platform_image.jpg");

            // verify
            Assert.NotNull(result);
            Assert.NotNull(result.Bitmap);
        }

        [Fact]
        public async Task FromFileAsync()
        {
            // prepare
            using var sp = CreateServices();
            var factory = sp.GetService<ToastImageSourceFactory>();
            var path = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "file.jpg");
            using (var file = File.Create(path))
            using (var rs = GetTestImageContent())
            {
                await rs.CopyToAsync(file);
                await file.FlushAsync();
            }

            // act
            var result = await factory.FromFileAsync(path);

            // verify
            Assert.NotNull(result);
            Assert.NotNull(result.Bitmap);
        }

        [Fact]
        public async Task FromUriAsync()
        {
            // prepare
            using var sp = CreateServices();
            var mockHcf = (MockHttpClientFactory)sp.GetService<IHttpClientFactory>();
            mockHcf.OnCreateHttpClient = name => new MockHttpClient(new HttpResponseMessage()
            {
                Content = new StreamContent(GetTestImageContent()),
                StatusCode = System.Net.HttpStatusCode.OK,
            });

            var factory = sp.GetService<ToastImageSourceFactory>();

            // act
            var result = await factory.FromUriAsync(new Uri("http://example.com", UriKind.Absolute));

            // verify
            Assert.NotNull(result);
            Assert.NotNull(result.Bitmap);
        }

        public static ServiceProvider CreateServices()
        {
            var sc = new ServiceCollection();
            sc.AddSingleton<IHttpClientFactory, MockHttpClientFactory>();
            sc.AddSingleton<ToastImageSourceFactory>();
            return sc.BuildServiceProvider();
        }

        static Stream GetTestImageContent() => Assembly.GetExecutingAssembly().GetManifestResourceStream(KResource);
    }
}
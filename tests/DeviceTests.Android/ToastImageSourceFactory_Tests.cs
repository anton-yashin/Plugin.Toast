﻿#nullable enable

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
using Plugin.Toast.Images;
using Plugin.Toast.Droid.Configuration;

namespace DeviceTests.Android
{
    public class ToastImageSourceFactory_Tests
    {
#if NET6_0_OR_GREATER
        const string KResource = "DeviceTests.Maui.Images.embedded_image.jpg";
#else
        const string KResource = "DeviceTests.Android.Images.embedded_image.jpg";
#endif

        [Fact]
        public async Task FromResourceAsync()
        {
            // prepare
            using var sp = CreateServices();
            var factory = sp.GetRequiredService<ToastImageSourceFactory>();

            // act
            var result = await factory.FromResourceAsync(KResource, this.GetType());

            // verify
            Assert.NotNull(result);
            Assert.NotNull(result.Bitmap);
        }

        [Fact]
        public async Task FromBundleAsync()
        {
            // prepare
            using var sp = CreateServices();
            var factory = sp.GetRequiredService<ToastImageSourceFactory>();

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
            var factory = sp.GetRequiredService<ToastImageSourceFactory>();
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
            var mockHcf = sp.GetRequiredService<Mock<IHttpClientFactory>>();
            mockHcf.Arrange(f => f.CreateClient(The<string>.IsAnyValue))
                .Returns((string name) => new MockHttpClient(new HttpResponseMessage()
                {
                    Content = new StreamContent(GetTestImageContent()),
                    StatusCode = System.Net.HttpStatusCode.OK,
                }));

            var factory = sp.GetRequiredService<ToastImageSourceFactory>();

            // act
            var result = await factory.FromUriAsync(new Uri("http://example.com", UriKind.Absolute));

            // verify
            Assert.NotNull(result);
            Assert.NotNull(result.Bitmap);
        }

        public static ServiceProvider CreateServices()
        {
            var sc = new ServiceCollection();
            sc.AddMock<IHttpClientFactory>();
            sc.AddSingleton<IActivityConfiguration>(sp
                => new ConfigurationBuilderExtensions.ActivityConfiguration(() => Platform.Activity));
            sc.AddSingleton<IPackageNameConfiguration>(sp
                => new ConfigurationBuilderExtensions.PackageNameConfiguration(Platform.Activity.PackageName ?? ""));
            sc.AddSingleton<ToastImageSourceFactory>();
            return sc.BuildServiceProvider();
        }

        static Stream GetTestImageContent() => Assembly.GetExecutingAssembly().GetManifestResourceStream(KResource);
    }
}
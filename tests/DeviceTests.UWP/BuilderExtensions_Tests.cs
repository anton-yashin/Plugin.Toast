using LightMock;
using Microsoft.Toolkit.Uwp.Notifications;
using Plugin.Toast;
using System;
using UnitTests.Mocks;
using Xunit;
using LightMock.Generator;

namespace DeviceTests.UWP
{
    public class BuilderExtensions_Tests
    {
        [Fact]
        public void AddImage()
        {
            // prepare
            var uri = new Uri("https://www.example.com/image.png");
            var mock = new Mock<IBuilder>();
            ToastImageSource ims = new SealedToastImageSource(uri);

            // act
            mock.Object.AddImage(ims);

            // verify
            mock.Assert(_ => _.Add(ims, Router.Route.Default));
        }

        [Fact]
        public void AddAppLogoOverride()
        {
            // prepare
            var uri = new Uri("https://www.example.com/image.png");
            var mock = new Mock<IUwpExtension>();
            var ims = new SealedToastImageSource(uri);

            // act
            mock.Object.AddAppLogoOverride(ims, ToastGenericAppLogoCrop.Circle, "alternateText", true);

            // verify
            mock.Assert(_ => _.AddAppLogoOverride(uri, new Nullable<ToastGenericAppLogoCrop>(ToastGenericAppLogoCrop.Circle), "alternateText", new Nullable<bool>(true)));
        }

        [Fact]
        public void AddHeroImage()
        {
            // prepare
            var uri = new Uri("https://www.example.com/image.png");
            var mock = new Mock<IUwpExtension>();
            var ims = new SealedToastImageSource(uri);

            // act
            mock.Object.AddHeroImage(ims, "alternate text", true);

            // verify
            mock.Assert(_ => _.AddHeroImage(uri, "alternate text", new Nullable<bool>(true)));
        }

        [Fact]
        public void AddInlineImage()
        {
            // prepare
            var uri = new Uri("https://www.example.com/image.png");
            var mock = new Mock<IUwpExtension>();
            var ims = new SealedToastImageSource(uri);

            // act
            mock.Object.AddInlineImage(ims, "alternate text", true, AdaptiveImageCrop.Circle, false);

            // verify
            mock.Assert(_ => _.AddInlineImage(uri, "alternate text", new Nullable<bool>(true), new Nullable<AdaptiveImageCrop>(AdaptiveImageCrop.Circle), new Nullable<bool>(false)));
        }
    }
}

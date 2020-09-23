using DeviceTests.UWP.Mocks;
using LightMock;
using Microsoft.Toolkit.Uwp.Notifications;
using Plugin.Toast;
using System;
using UnitTests.Mocks;
using Xunit;

namespace DeviceTests.UWP
{
    public class BuilderExtensions_Tests
    {
        [Fact]
        public void AddImage()
        {
            // prepare
            var uri = new Uri("https://www.example.com/image.png");
            var mockContext = new MockContext<IBuilder>();
            var extensionMock = new MockBulider(mockContext);
            ToastImageSource ims = new SealedToastImageSource(uri);

            // act
            extensionMock.AddImage(ims);

            // verify
            mockContext.Assert(_ => _.Add(ims, Router.Route.Default));
        }

        [Fact]
        public void AddAppLogoOverride()
        {
            // prepare
            var uri = new Uri("https://www.example.com/image.png");
            var mockContext = new MockContext<IUwpExtension>();
            var extensionMock = new MockUwpExtension(mockContext);
            var ims = new SealedToastImageSource(uri);

            // act
            extensionMock.AddAppLogoOverride(ims, ToastGenericAppLogoCrop.Circle, "alternateText", true);

            // verify
            mockContext.Assert(_ => _.AddAppLogoOverride(uri, new Nullable<ToastGenericAppLogoCrop>(ToastGenericAppLogoCrop.Circle), "alternateText", new Nullable<bool>(true)));
        }

        [Fact]
        public void AddHeroImage()
        {
            // prepare
            var uri = new Uri("https://www.example.com/image.png");
            var mockContext = new MockContext<IUwpExtension>();
            var extensionMock = new MockUwpExtension(mockContext);
            var ims = new SealedToastImageSource(uri);

            // act
            extensionMock.AddHeroImage(ims, "alternate text", true);

            // verify
            mockContext.Assert(_ => _.AddHeroImage(uri, "alternate text", new Nullable<bool>(true)));
        }

        [Fact]
        public void AddInlineImage()
        {
            // prepare
            var uri = new Uri("https://www.example.com/image.png");
            var mockContext = new MockContext<IUwpExtension>();
            var extensionMock = new MockUwpExtension(mockContext);
            var ims = new SealedToastImageSource(uri);

            // act
            extensionMock.AddInlineImage(ims, "alternate text", true, AdaptiveImageCrop.Circle, false);

            // verify
            mockContext.Assert(_ => _.AddInlineImage(uri, "alternate text", new Nullable<bool>(true), new Nullable<AdaptiveImageCrop>(AdaptiveImageCrop.Circle), new Nullable<bool>(false)));
        }
    }
}

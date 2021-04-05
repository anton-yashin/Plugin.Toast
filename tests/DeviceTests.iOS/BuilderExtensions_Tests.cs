using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Plugin.Toast;
using UserNotifications;
using Xunit;
using LightMock.Generator;

namespace DeviceTests.iOS
{
    public class BuilderExtensions_Tests
    {
        [Fact]
        public void AddImage()
        {
            // prepare
            var attachment = UNNotificationAttachment.FromIdentifier(
                Guid.NewGuid().ToString(),
                NSUrl.FromFilename("filename"),
                new UNNotificationAttachmentOptions(),
                out var error);
            var mock = new Mock<IIosNotificationExtension>();
            var extension = mock.Object;
            ToastImageSource ims = new SealedToastImageSource(attachment);

            // act
            extension.AddImage(ims);

            // verify
            mock.Assert(_ => _.Add(ims, Router.Route.Default));
        }

        [Fact]
        public void AddAttachment()
        {
            // prepare
            var attachment = UNNotificationAttachment.FromIdentifier(
                Guid.NewGuid().ToString(),
                NSUrl.FromFilename("filename"),
                new UNNotificationAttachmentOptions(),
                out var error);
            var mock = new Mock<IIosNotificationExtension>();
            var extension = mock.Object;
            ToastImageSource ims = new SealedToastImageSource(attachment);

            // act
            extension.AddAttachment(ims);

            // verify
            mock.Assert(_ => _.Add(ims, Router.Route.IosSingleAttachment));
        }

        [Fact]
        public void AddAttachments()
        {
            // prepare
            var attachment = UNNotificationAttachment.FromIdentifier(
                Guid.NewGuid().ToString(),
                NSUrl.FromFilename("filename"),
                new UNNotificationAttachmentOptions(),
                out var error);
            var mock = new Mock<IIosNotificationExtension>();
            var extension = mock.Object;
            ToastImageSource ims = new SealedToastImageSource(attachment);
            IEnumerable<ToastImageSource> images = Enumerable.Repeat(ims, 10);

            // act
            extension.AddAttachments(images);

            // verify
            mock.Assert(_ => _.Add(images, Router.Route.IosMultipleAttachments));
        }

    }
}
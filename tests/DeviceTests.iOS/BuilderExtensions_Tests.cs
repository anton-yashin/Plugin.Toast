using System;
using System.Collections.Generic;
using System.Linq;
using DeviceTests.iOS.Mocks;
using Foundation;
using Plugin.Toast;
using UserNotifications;
using Xunit;

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
            var mock = new MockIosNotificationExtension();
            var extension = mock.CommonObject;
            ToastImageSource ims = new SealedToastImageSource(attachment);

            // act
            extension.AddImage(ims);

            // verify
            mock.Context.Assert(_ => _.Add(ims, Router.Route.Default));
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
            var mock = new MockIosNotificationExtension();
            var extension = mock.CommonObject;
            ToastImageSource ims = new SealedToastImageSource(attachment);

            // act
            extension.AddAttachment(ims);

            // verify
            mock.Context.Assert(_ => _.Add(ims, Router.Route.IosSingleAttachment));
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
            var mock = new MockIosNotificationExtension();
            var extension = mock.CommonObject;
            ToastImageSource ims = new SealedToastImageSource(attachment);
            IEnumerable<ToastImageSource> images = Enumerable.Repeat(ims, 10);

            // act
            extension.AddAttachments(images);

            // verify
            mock.Context.Assert(_ => _.Add(images, Router.Route.IosMultipleAttachments));
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using DeviceTests.iOS.Mocks;
using DeviceTests.Utils;
using Foundation;
using Plugin.Toast;
using UserNotifications;
using Xunit;

namespace DeviceTests.iOS
{
    public class IosImageRouter_Tests
    {
        [Theory, MemberData(nameof(GetInvalidRoutesForSingle))]
        public void ThrowsExceptionOnUnknownRouteForSingle(object route)
        {
            // prepare
            var attachment = UNNotificationAttachment.FromIdentifier(
                Guid.NewGuid().ToString(),
                NSUrl.FromFilename("filename"),
                new UNNotificationAttachmentOptions(),
                out var error);
            var router = new IOsImageRouter();
            var mock = new MockIosNotificationExtension();
            var ims = new SealedToastImageSource(attachment);

            // act && verify
            Assert.Throws<InvalidOperationException>(() => router.Configure(mock.SpecificObject, ims, (Router.Route)route));
        }

        public static IEnumerable<object[]> GetInvalidRoutesForSingle()
            => EnumUtils.GetEnumValuesExclude(Router.Route.Default, Router.Route.IosSingleAttachment).Select(i => new object[] { i });

        [Theory, MemberData(nameof(GetInvalidRoutesForMultiple))]
        public void ThrowsExceptionOnUnknownRouteForMultiple(object route)
        {
            // prepare
            var attachment = UNNotificationAttachment.FromIdentifier(
                Guid.NewGuid().ToString(),
                NSUrl.FromFilename("filename"),
                new UNNotificationAttachmentOptions(),
                out var error);
            var router = new IOsImageRouter();
            var mock = new MockIosNotificationExtension();
            var ims = new SealedToastImageSource(attachment);
            var many = Enumerable.Repeat(ims, 10);

            // act && verify
            Assert.Throws<InvalidOperationException>(() => router.Configure(mock.SpecificObject, many, (Router.Route)route));
        }

        public static IEnumerable<object[]> GetInvalidRoutesForMultiple()
            => EnumUtils.GetEnumValuesExclude(Router.Route.IosMultipleAttachments).Select(i => new object[] { i });

        [Theory, InlineData(Router.Route.Default), InlineData(Router.Route.IosSingleAttachment)]
        public void ConfigureSingle(object route)
        {
            // prepare
            var attachment = UNNotificationAttachment.FromIdentifier(
                Guid.NewGuid().ToString(),
                NSUrl.FromFilename("filename"),
                new UNNotificationAttachmentOptions(),
                out var error);
            var router = new IOsImageRouter();
            var mock = new MockIosNotificationExtension();
            ToastImageSource ims = new SealedToastImageSource(attachment);

            // act
            router.Configure(mock.SpecificObject, ims, (Router.Route)route);

            // verify
            mock.Platform.Assert(_ => _.AddAttachment(attachment));
        }

        [Theory, InlineData(Router.Route.IosMultipleAttachments)]
        public void ConfigureMultiple(object route)
        {
            // prepare
            var attachment = UNNotificationAttachment.FromIdentifier(
                Guid.NewGuid().ToString(),
                NSUrl.FromFilename("filename"),
                new UNNotificationAttachmentOptions(),
                out var error);
            var router = new IOsImageRouter();
            var mock = new MockIosNotificationExtension();
            ToastImageSource ims = new SealedToastImageSource(attachment);
            var many = Enumerable.Repeat(ims, 10);

            // act
            router.Configure(mock.SpecificObject, many, (Router.Route)route);

            // verify
            mock.Platform.Assert(_ => _.AddAttachments(many.Select(i => i.Attachment)));
        }
    }
}
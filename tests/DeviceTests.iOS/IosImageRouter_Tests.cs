using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeviceTests.iOS.Mocks;
using Foundation;
using Plugin.Toast;
using UIKit;
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
        {
            foreach (Router.Route i in Enum.GetValues(typeof(Router.Route)))
            {
                switch (i)
                {
                    case Router.Route.Default:
                    case Router.Route.IosSingleAttachment:
                        break;
                    default:
                        yield return new object[] { i };
                        break;
                }
            }
        }

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
        {
            foreach (Router.Route i in Enum.GetValues(typeof(Router.Route)))
            {
                switch (i)
                {
                    case Router.Route.IosMultipleAttachments:
                        break;
                    default:
                        yield return new object[] { i };
                        break;
                }
            }
        }

        [Theory, MemberData(nameof(GetValidRoutesForSingle))]
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

        public static IEnumerable<object[]> GetValidRoutesForSingle()
        {
            foreach (Router.Route i in Enum.GetValues(typeof(Router.Route)))
            {
                switch (i)
                {
                    case Router.Route.Default:
                    case Router.Route.IosSingleAttachment:
                        yield return new object[] { i };
                        break;
                }
            }
        }

        [Theory, MemberData(nameof(GetValidRoutesForMultiple))]
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

        public static IEnumerable<object[]> GetValidRoutesForMultiple()
        {
            foreach (Router.Route i in Enum.GetValues(typeof(Router.Route)))
            {
                switch (i)
                {
                    case Router.Route.IosMultipleAttachments:
                        yield return new object[] { i };
                        break;
                }
            }
        }

    }
}
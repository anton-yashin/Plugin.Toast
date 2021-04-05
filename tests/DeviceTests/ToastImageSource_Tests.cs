#if NETCORE_APP == false
#nullable enable

using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DeviceTests
{
    public class ToastImageSource_Tests
    {
        [Fact]
        public void Construct()
        {
#if __ANDROID__
            var expected = global::Android.Graphics.Bitmap.CreateBitmap(100, 100, global::Android.Graphics.Bitmap.Config.Alpha8)
                ?? throw new InvalidOperationException();
            var tis = new SealedToastImageSource(expected);
            Assert.Equal(expected, tis.Bitmap);
#elif __IOS__
            var expected = UserNotifications.UNNotificationAttachment.FromIdentifier(
                Guid.NewGuid().ToString(),
                new Foundation.NSUrl("https://picsum.photos/200"),
                new UserNotifications.UNNotificationAttachmentOptions()
                {
                    TypeHint = "public.jpg"
                }, out var _);
            var tis = new SealedToastImageSource(expected);
            Assert.Equal(expected, tis.Attachment);
#elif NETFX_CORE
            var expected = new Uri("https://picsum.photos/200", UriKind.Absolute);
            var tis = new SealedToastImageSource(expected);
            Assert.Equal(expected, tis.ImageUri);
#else
            throw new PlatformNotSupportedException();
#endif
        }
    }
}

#endif
#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeviceTests.Android.Mocks;
using LightMock;
using Plugin.Toast;
using Plugin.Toast.Droid;
using Xunit;

namespace DeviceTests.Android
{
    public class DroidImageRouter_Tests
    {
        [Theory, MemberData(nameof(GetInvalidRoutes))]
        public void ThrowsExceptionOnUnknownRoute(object route)
        {
            // prepare
            var bitmap = Bitmap.CreateBitmap(100, 100, Bitmap.Config.Alpha8)
                ?? throw new InvalidOperationException("can't create a test bitmap");
            var router = new DroidImageRouter();
            var mockContext = new MockContext<IDroidNotificationExtension>();
            var platformContext = new MockContext<IPlatformSpecificExtension>();
            var mockExtension = new MockDroidNotificationExtension(mockContext, platformContext);
            var ims = new SealedToastImageSource(bitmap);

            // act && verify
            Assert.Throws<InvalidOperationException>(() => router.Configure(mockExtension, ims, (Router.Route)route));
        }

        public static IEnumerable<object[]> GetInvalidRoutes()
        {
            foreach (Router.Route i in Enum.GetValues(typeof(Router.Route)))
            {
                switch (i)
                {
                    case Router.Route.Default:
                    case Router.Route.DroidLargeIcon:
                        break;
                    default:
                        yield return new object[] { i };
                        break;
                }
            }
        }

        [Theory, MemberData(nameof(GetValidRoutes))]
        public void Configure(object route)
        {
            // prepare
            var bitmap = Bitmap.CreateBitmap(100, 100, Bitmap.Config.Alpha8)
                ?? throw new InvalidOperationException("can't create a test bitmap");
            var router = new DroidImageRouter();
            var mockContext = new MockContext<IDroidNotificationExtension>();
            var platformContext = new MockContext<IPlatformSpecificExtension>();
            var mockExtension = new MockDroidNotificationExtension(mockContext, platformContext);
            ToastImageSource ims = new SealedToastImageSource(bitmap);

            // act
            router.Configure(mockExtension, ims, (Router.Route)route);

            // verify
            platformContext.Assert(_ => _.SetLargeIcon(bitmap));
        }

        public static IEnumerable<object[]> GetValidRoutes()
        {
            foreach (Router.Route i in Enum.GetValues(typeof(Router.Route)))
            {
                switch (i)
                {
                    case Router.Route.Default:
                    case Router.Route.DroidLargeIcon:
                        yield return new object[] { i };
                        break;
                }
            }
        }

    }
}
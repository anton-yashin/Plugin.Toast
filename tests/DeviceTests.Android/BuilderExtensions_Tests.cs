#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeviceTests.Android.Mocks;
using LightMock;
using Plugin.Toast;
using Plugin.Toast.Droid;
using UnitTests.Mocks;
using Xunit;
using ABitmap = global::Android.Graphics.Bitmap;

namespace DeviceTests.Android
{
    public class BuilderExtensions_Tests
    {
        [Fact]
        public void AddImage()
        {
            // prepare
            var bitmap = ABitmap.CreateBitmap(100, 100, ABitmap.Config.Alpha8)
                ?? throw new InvalidOperationException("can't create a test bitmap");
            var mockContext = new MockContext<IBuilder>();
            var extensionMock = new MockBulider(mockContext);
            ToastImageSource ims = new SealedToastImageSource(bitmap);

            // act
            extensionMock.AddImage(ims);

            // verify
            mockContext.Assert(_ => _.Add(ims, Router.Route.Default));
        }

        [Fact]
        public void AddLargeIcon()
        {
            // prepare
            var bitmap = ABitmap.CreateBitmap(100, 100, ABitmap.Config.Alpha8)
                ?? throw new InvalidOperationException("can't create a test bitmap");
            var droidContext = new MockContext<IDroidNotificationExtension>();
            var extensionMock = new MockDroidNotificationExtension(droidContext);
            var extension = (IDroidNotificationExtension)extensionMock;
            ToastImageSource ims = new SealedToastImageSource(bitmap);

            // act
            extension.AddLargeIcon(ims);

            // verify
            droidContext.Assert(_ => _.Add(ims, Router.Route.DroidLargeIcon));
        }
    }
}
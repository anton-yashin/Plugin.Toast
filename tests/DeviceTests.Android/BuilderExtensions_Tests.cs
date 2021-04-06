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
using LightMock;
using LightMock.Generator;
using Plugin.Toast;
using Plugin.Toast.Droid;
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
            var bitmap = ABitmap.CreateBitmap(100, 100, ABitmap.Config.Alpha8!)
                ?? throw new InvalidOperationException("can't create a test bitmap");
            var mock = new Mock<IBuilder>();
            ToastImageSource ims = new SealedToastImageSource(bitmap);

            // act
            mock.Object.AddImage(ims);

            // verify
            mock.Assert(_ => _.Add(ims, Router.Route.Default));
        }

        [Fact]
        public void AddLargeIcon()
        {
            // prepare
            var bitmap = ABitmap.CreateBitmap(100, 100, ABitmap.Config.Alpha8!)
                ?? throw new InvalidOperationException("can't create a test bitmap");
            var mock = new Mock<IDroidNotificationExtension>();
            var extension = (IDroidNotificationExtension)mock.Object;
            ToastImageSource ims = new SealedToastImageSource(bitmap);

            // act
            extension.AddLargeIcon(ims);

            // verify
            mock.Assert(_ => _.Add(ims, Router.Route.DroidLargeIcon));
        }
    }
}
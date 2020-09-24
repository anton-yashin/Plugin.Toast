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
using DeviceTests.Utils;
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
            => EnumUtils.GetEnumValuesExclude(Router.Route.Default, Router.Route.DroidLargeIcon).Select(i => new object[] { i });

        [Theory, InlineData(Router.Route.Default), InlineData(Router.Route.DroidLargeIcon)]
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
    }
}
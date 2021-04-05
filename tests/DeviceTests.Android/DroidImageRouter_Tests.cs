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
using DeviceTests.Utils;
using LightMock;
using LightMock.Generator;
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
            var bitmap = Bitmap.CreateBitmap(100, 100, Bitmap.Config.Alpha8!)
                ?? throw new InvalidOperationException("can't create a test bitmap");
            var router = new DroidImageRouter();
            var mock = new Mock<IPlatformSpecificExtension>();
            var ims = new SealedToastImageSource(bitmap);

            // act && verify
            Assert.Throws<InvalidOperationException>(() => router.Configure(mock.Object, ims, (Router.Route)route));
        }

        public static IEnumerable<object[]> GetInvalidRoutes()
            => EnumUtils.GetEnumValuesExclude(Router.Route.Default, Router.Route.DroidLargeIcon).Select(i => new object[] { i });

        [Theory, InlineData(Router.Route.Default), InlineData(Router.Route.DroidLargeIcon)]
        public void Configure(object route)
        {
            // prepare
            var bitmap = Bitmap.CreateBitmap(100, 100, Bitmap.Config.Alpha8!)
                ?? throw new InvalidOperationException("can't create a test bitmap");
            var router = new DroidImageRouter();
            var mock = new Mock<IPlatformSpecificExtension>();
            ToastImageSource ims = new SealedToastImageSource(bitmap);

            // act
            router.Configure(mock.Object, ims, (Router.Route)route);

            // verify
            mock.Assert(_ => _.SetLargeIcon(bitmap));
        }
    }
}
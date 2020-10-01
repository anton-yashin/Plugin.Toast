#nullable enable
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Toast;
using Xunit;

namespace DeviceTests.Android
{
    public class ToastId_Tests
    {
        [Theory, MemberData(nameof(GetEqualsData))]
        public void Equals_(int id, string tag)
        {
            // prepare
            var left = new ToastId(id, tag);
            var right = new ToastId(id, tag);

            // act & verify
            Assert.Equal(left, right);
            Assert.Equal(left.GetHashCode(), right.GetHashCode());
        }

        static IEnumerable<object?[]> GetEqualsData()
        {
            var r = new Random();
            yield return new object?[] { r.Next(), Guid.NewGuid().ToString() };
            yield return new object?[] { r.Next(), null };
        }
    }
}
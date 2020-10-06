#nullable enable
using Plugin.Toast;
using System;
using System.Collections.Generic;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Xunit;

namespace DeviceTests.UWP
{
    public class ToastId_Tests
    {
        [Theory, MemberData(nameof(GetEqualsData))]
        public void Equals_(string tag, string group)
        {
            // prepare
            var left = new ToastId(tag, group);
            var right = new ToastId(tag, group);

            // act & verify
            Assert.Equal(left, right);
            Assert.Equal(left.GetHashCode(), right.GetHashCode());
        }

        static IEnumerable<object?[]> GetEqualsData()
        {
            yield return new object?[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString()};
            yield return new object?[] { null, Guid.NewGuid().ToString() };
            yield return new object?[] { Guid.NewGuid().ToString(), null };
            yield return new object?[] { null, null };
        }

        [Fact]
        public void FromToastNotification()
        {
            var tn = new ToastNotification(new XmlDocument())
            {
                Tag = Guid.NewGuid().ToString(),
                Group = Guid.NewGuid().ToString(),
            };

            var toastId = ToastId.FromNotification(tn);

            Assert.Equal(tn.Tag, toastId.Tag);
            Assert.Equal(tn.Group, toastId.Group);
        }

        [Fact]
        public void FromScheduledToastNotification()
        {
            var tn = new ScheduledToastNotification(new XmlDocument(), DateTimeOffset.Now + TimeSpan.FromDays(1))
            {
                Tag = Guid.NewGuid().ToString(),
                Group = Guid.NewGuid().ToString(),
            };

            var toastId = ToastId.FromNotification(tn);

            Assert.Equal(tn.Tag, toastId.Tag);
            Assert.Equal(tn.Group, toastId.Group);
        }

        [Theory, MemberData(nameof(GetTestData_GetPersistentHashCode))]
        public void GetPersistentHashCode(int expected, string tag, string group)
        {
            // preapre
            var tid = new ToastId(tag, group);

            // act & verify
            Assert.Equal(expected, tid.GetPersistentHashCode());
        }

        static IEnumerable<object?[]> GetTestData_GetPersistentHashCode()
        {
            yield return new object?[] { 1558670046, null, null };
            yield return new object?[] { 1314885743, "ab", "cde" };
            yield return new object?[] { -105462072, "ab", null };
            yield return new object?[] { 801742924, null, "cde" };
        }

    }
}

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
        public void Equals_(string tag, string group, ToastIdNotificationType notificationType)
        {
            // prepare
            var left = new ToastId(tag, group, notificationType);
            var right = new ToastId(tag, group, notificationType);

            // act & verify
            Assert.Equal(left, right);
            Assert.Equal(left.GetHashCode(), right.GetHashCode());
        }

        static IEnumerable<object?[]> GetEqualsData()
        {
            yield return new object?[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), ToastIdNotificationType.Unknown };
            yield return new object?[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), ToastIdNotificationType.ToastNotification };
            yield return new object?[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), ToastIdNotificationType.ScheduledToastNotification };
            yield return new object?[] { null, Guid.NewGuid().ToString(), ToastIdNotificationType.Unknown };
            yield return new object?[] { null, Guid.NewGuid().ToString(), ToastIdNotificationType.ToastNotification };
            yield return new object?[] { null, Guid.NewGuid().ToString(), ToastIdNotificationType.ScheduledToastNotification };
            yield return new object?[] { Guid.NewGuid().ToString(), null, ToastIdNotificationType.Unknown };
            yield return new object?[] { Guid.NewGuid().ToString(), null, ToastIdNotificationType.ToastNotification };
            yield return new object?[] { Guid.NewGuid().ToString(), null, ToastIdNotificationType.ScheduledToastNotification };
            yield return new object?[] { null, null, ToastIdNotificationType.Unknown };
            yield return new object?[] { null, null, ToastIdNotificationType.ToastNotification };
            yield return new object?[] { null, null, ToastIdNotificationType.ScheduledToastNotification };
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
            Assert.Equal(ToastIdNotificationType.ToastNotification, toastId.NotificationType);
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
            Assert.Equal(ToastIdNotificationType.ScheduledToastNotification, toastId.NotificationType);
        }

        [Theory, MemberData(nameof(GetTestData_GetPersistentHashCode))]
        public void GetPersistentHashCode(int expected, string tag, string group, object notificationType)
        {
            // preapre
            var tid = new ToastId(tag, group, (ToastIdNotificationType)notificationType);

            // act & verify
            Assert.Equal(expected, tid.GetPersistentHashCode());
        }

        static IEnumerable<object?[]> GetTestData_GetPersistentHashCode()
        {
            yield return new object?[] { -103496021, null, null, ToastIdNotificationType.ScheduledToastNotification };
            yield return new object?[] { 441556569, "ab", "cde", ToastIdNotificationType.ToastNotification };
            yield return new object?[] { 814718951, "ab", null, ToastIdNotificationType.Unknown };
            yield return new object?[] { 687712723, null, "cde", ToastIdNotificationType.ScheduledToastNotification };
        }

    }
}

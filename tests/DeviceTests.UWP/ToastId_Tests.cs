using Plugin.Toast;
using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Xunit;

namespace DeviceTests.UWP
{
    public class ToastId_Tests
    {
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
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Toast;
using Xunit;

namespace DeviceTests
{
    public class BuilderTests
    {
        [Fact]
        public void WhenUsingDefaultExtensions()
        {
            bool platformInvoked = false;
            bool invalidPlatformInvoked = false;
            var nm = Platform.CreateNotificationManager();
            var builder = nm.BuildNotification();

            builder.WhenUsing<IDroidNotificationExtension>(_ => platformInvoked = true)
                .WhenUsing<ISnackbarExtension>(_ => invalidPlatformInvoked = true)
                .WhenUsing<IIosNotificationExtension>(_ => platformInvoked = true)
                .WhenUsing<IIosLocalNotificationExtension>(_ => invalidPlatformInvoked = true)
                .WhenUsing<IUwpExtension>(_ => platformInvoked = true);

            Assert.False(invalidPlatformInvoked);
            Assert.True(platformInvoked);
        }

        [Fact]
        public void WhenUsingSpecificExtensions()
        {
            bool platformInvoked = false;
            bool invalidPlatformInvoked = false;
            var nm = Platform.CreateNotificationManager();
            var builder = nm.BuildNotificationUsing<ISnackbarExtension, IIosLocalNotificationExtension>();

            builder.WhenUsing<IDroidNotificationExtension>(_ => invalidPlatformInvoked = true)
                .WhenUsing<ISnackbarExtension>(_ => platformInvoked = true)
                .WhenUsing<IIosNotificationExtension>(_ => invalidPlatformInvoked = true)
                .WhenUsing<IIosLocalNotificationExtension>(_ => platformInvoked = true)
                .WhenUsing<IUwpExtension>(_ => platformInvoked = true);

            Assert.False(invalidPlatformInvoked);
            Assert.True(platformInvoked);
        }

        [Fact]
        public void BuildTwiceMustFail()
        {
            var nm = Platform.CreateNotificationManager();
            var builder = nm.BuildNotification();
            builder.Build();

            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

        [Fact]
        public void BuildTwiceMustFailAlternative()
        {
            var nm = Platform.CreateNotificationManager();
            var builder = nm.BuildNotificationUsing<ISnackbarExtension, IIosLocalNotificationExtension>();
            builder.Build();

            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }
    }
}

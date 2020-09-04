using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Toast;
using Xunit;

namespace DeviceTests
{
    public class ShowNotificationTests
    {
        [Fact]
        public Task CancellationTokenShowDefault()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                var nm = Platform.CreateNotificationManager();
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
                await Assert.ThrowsAsync<TaskCanceledException>(()
                    => nm.BuildNotification().AddTitle("title").AddDescription("please ignore")
                    .Build().ShowAsync(cts.Token));
            });

        [Fact]
        public Task CancellationTokenShowAlternative()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                var nm = Platform.CreateNotificationManager();
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
                await Assert.ThrowsAsync<TaskCanceledException>(()
                    => nm.BuildNotificationUsing<ISnackbarExtension, IIosLocalNotificationExtension>()
                    .AddTitle("title").AddDescription("please ignore")
                    .Build().ShowAsync(cts.Token));
            });

        [Fact]
        public Task ShowWithDelay()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                var nm = Platform.CreateNotificationManager();
                using var token = nm.BuildNotification().AddTitle("scheduled").AddDescription("please ignore")
                    .Build().ScheduleTo(DateTimeOffset.Now + TimeSpan.FromSeconds(2));
                await Task.Delay(TimeSpan.FromSeconds(3));
            });

        [Fact]
        public Task ShowWithDelayAlternative()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                var nm = Platform.CreateNotificationManager();
                using var token = nm.BuildNotificationUsing<ISnackbarExtension, IIosLocalNotificationExtension>()
                    .AddTitle("scheduled alt").AddDescription("please ignore")
                    .Build().ScheduleTo(DateTimeOffset.Now + TimeSpan.FromSeconds(2));
                await Task.Delay(TimeSpan.FromSeconds(3));
            });
    }
}

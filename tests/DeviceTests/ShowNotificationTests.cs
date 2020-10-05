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
        const string KRunningTest = "running test please ignore";

        [Fact]
        public Task CancellationTokenShowDefault()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                var nm = Platform.CreateNotificationManager();
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
                var task = nm.GetBuilder().AddTitle(nameof(CancellationTokenShowDefault)).AddDescription(KRunningTest)
                    .Build().ShowAsync(cts.Token);
                await Assert.ThrowsAsync<TaskCanceledException>(() => task);
            });

#if __IOS__

        [Fact]
        public Task LocalNotificationHaveUnknownResult()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                var nm = Platform.CreateNotificationManager();
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
                await Task.Delay(TimeSpan.FromSeconds(5));
                var result = await nm.GetBuilder<IIosLocalNotificationExtension>()
                    .AddTitle(nameof(LocalNotificationHaveUnknownResult)).AddDescription(KRunningTest)
                    .Build().ShowAsync(cts.Token);
                Assert.Equal(expected: NotificationResult.Unknown, result);
            });

#else

        [Fact]
        public async Task CancellationTokenShowAlternative()
        {
            var nm = Platform.CreateNotificationManager();
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
            await Task.Delay(TimeSpan.FromSeconds(5));
            await Assert.ThrowsAsync<TaskCanceledException>(() => 
            nm.GetBuilder<ISnackbarExtension, IIosLocalNotificationExtension>()
                .AddTitle(nameof(CancellationTokenShowAlternative)).AddDescription(KRunningTest)
                .Build().ShowAsync(cts.Token));
        }

#endif

        [Fact]
        public async Task ShowWithDelay()
        {
            await Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                var nm = Platform.CreateNotificationManager();
                using var token = nm.GetBuilder().AddTitle(nameof(ShowWithDelay)).AddDescription(KRunningTest)
                    .Build().ScheduleTo(DateTimeOffset.Now + TimeSpan.FromSeconds(2));
                await Task.Delay(TimeSpan.FromSeconds(3));
            });
        }

        [Fact]
        public async Task ShowWithDelayAlternative()
        {
            await Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                var nm = Platform.CreateNotificationManager();
                using var token = nm.GetBuilder<ISnackbarExtension, IIosLocalNotificationExtension>()
                    .AddTitle(nameof(ShowWithDelayAlternative)).AddDescription(KRunningTest)
                    .Build().ScheduleTo(DateTimeOffset.Now + TimeSpan.FromSeconds(2));
                await Task.Delay(TimeSpan.FromSeconds(3));
            });
        }
    }
}

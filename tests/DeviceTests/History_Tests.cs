using Plugin.Toast;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DeviceTests
{
    public class History_Tests
    {
        [Fact]
        public Task IsDeliveredAndRemove()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                // prepare
                var nm = Platform.CreateNotificationManager();
                var history = Platform.CreateHistory();

                var task = nm.GetBuilder().AddTitle("some title").Build()
                    .ShowAsync(out var toastId);

                await Task.Delay(100);

                var delivered = await history.IsDeliveredAsync(toastId);
                Assert.True(delivered);

                // act
                history.Remove(toastId);
                delivered = await history.IsDeliveredAsync(toastId);

                //verify
                Assert.False(delivered);
                await task;
            });

        [Fact]
        public Task IsDeliveredAndRemoveWithUwpGroup()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                // prepare
                var nm = Platform.CreateNotificationManager();
                var history = Platform.CreateHistory();
                var group = Guid.NewGuid().ToString();

                var task = nm.GetBuilder().AddTitle("some title")
                    .WhenUsing<IUwpExtension>(u => u.SetGroup(group)).Build()
                    .ShowAsync(out var toastId);

                await Task.Delay(100);

                var delivered = await history.IsDeliveredAsync(toastId);
                Assert.True(delivered);

                // act
                history.Remove(toastId);
                delivered = await history.IsDeliveredAsync(toastId);

                //verify
                Assert.False(delivered);
                await task;
            });

        [Fact]
        public Task RemoveAll()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                // prepare
                var nm = Platform.CreateNotificationManager();
                var history = Platform.CreateHistory();

                var task = nm.GetBuilder().AddTitle("some title").Build()
                    .ShowAsync(out var toastId);

                await Task.Delay(100);

                var delivered = await history.IsDeliveredAsync(toastId);
                Assert.True(delivered);

                // act
                history.RemoveAll();
                delivered = await history.IsDeliveredAsync(toastId);

                //verify
                Assert.False(delivered);
                await task;
            });

    }
}

using Plugin.Toast;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DeviceTests
{
    public class History_Tests
    {
        const string KRunningTest = "running test please ignore";

        [Fact]
        public Task IsDeliveredAndRemove()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                // prepare
                var nm = Platform.CreateNotificationManager();
                var history = Platform.CreateHistory();

                var task = await nm.GetBuilder().AddTitle(nameof(IsDeliveredAndRemove)).AddDescription(KRunningTest).Build()
                    .ShowAsync(out var toastId);

                var delivered = await history.IsDeliveredAsync(toastId);
                Assert.True(delivered);

                // act
                history.Remove(toastId);
                delivered = await history.IsDeliveredAsync(toastId);

                //verify
                Assert.False(delivered);
            });

        [Fact]
        public Task IsDeliveredAndRemoveWithUwpGroup()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                // prepare
                var nm = Platform.CreateNotificationManager();
                var history = Platform.CreateHistory();
                var group = Guid.NewGuid().ToString();

                var result = await nm.GetBuilder().AddTitle(nameof(IsDeliveredAndRemoveWithUwpGroup))
                    .AddDescription(KRunningTest)
                    .WhenUsing<IUwpExtension>(u => u.SetGroup(group)).Build()
                    .ShowAsync(out var toastId);

                var delivered = await history.IsDeliveredAsync(toastId);
                Assert.True(delivered);

                // act
                history.Remove(toastId);
                delivered = await history.IsDeliveredAsync(toastId);

                //verify
                Assert.False(delivered);
            });

        [Fact]
        public Task RemoveAll()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                // prepare
                var nm = Platform.CreateNotificationManager();
                var history = Platform.CreateHistory();

                var result = await nm.GetBuilder().AddTitle(nameof(RemoveAll))
                    .AddDescription(KRunningTest).Build()
                    .ShowAsync(out var toastId);

                var delivered = await history.IsDeliveredAsync(toastId);
                Assert.True(delivered);

                // act
                history.RemoveAll();
                delivered = await history.IsDeliveredAsync(toastId);

                //verify
                Assert.False(delivered);
            });

    }
}

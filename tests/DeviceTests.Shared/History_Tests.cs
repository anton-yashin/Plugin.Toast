#if NETCORE_APP == false
#nullable enable

using Microsoft.Extensions.DependencyInjection;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
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
                using var sc = CreateServices();
                var nm = sc.GetRequiredService<INotificationManager>();
                var history = sc.GetRequiredService<IHistory>();

                var task = await nm.GetBuilder().AddTitle(nameof(IsDeliveredAndRemove)).AddDescription(KRunningTest).Build()
                    .ShowAsync(out var toastId);

                var delivered = await history.IsDeliveredAsync(toastId);
                Assert.True(delivered);

                // act
                history.RemoveDelivered(toastId);
                delivered = await history.IsDeliveredAsync(toastId);

                //verify
                Assert.False(delivered);
            });

        [Fact]
        public Task IsDeliveredAndRemoveWithUwpGroup()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                // prepare
                using var sc = CreateServices();
                var nm = sc.GetRequiredService<INotificationManager>();
                var history = sc.GetRequiredService<IHistory>();
                var group = Guid.NewGuid().ToString();

                var result = await nm.GetBuilder().AddTitle(nameof(IsDeliveredAndRemoveWithUwpGroup))
                    .AddDescription(KRunningTest)
                    .WhenUsing<IUwpExtension>(u => u.SetGroup(group)).Build()
                    .ShowAsync(out var toastId);

                var delivered = await history.IsDeliveredAsync(toastId);
                Assert.True(delivered);

                // act
                history.RemoveDelivered(toastId);
                delivered = await history.IsDeliveredAsync(toastId);

                //verify
                Assert.False(delivered);
            });

        [Fact]
        public Task RemoveAll()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                // prepare
                using var sc = CreateServices();
                var nm = sc.GetRequiredService<INotificationManager>();
                var history = sc.GetRequiredService<IHistory>();

                var result = await nm.GetBuilder().AddTitle(nameof(RemoveAll))
                    .AddDescription(KRunningTest).Build()
                    .ShowAsync(out var toastId);

                var delivered = await history.IsDeliveredAsync(toastId);
                Assert.True(delivered);

                // act
                history.RemoveAllDelivered();
                delivered = await history.IsDeliveredAsync(toastId);

                //verify
                Assert.False(delivered);
            });


        [Fact]
        public Task IsScheduledAsync()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                using (var sp = CreateServices())
                {
                    ToastId toastId;
                    var history = sp.GetRequiredService<IHistory>();
                    var randomId = GetRandomScheduledToastId();
                    Assert.False(await history.IsScheduledAsync(randomId), "random id found");
                    await sp.GetRequiredService<IInitialization>().InitializeAsync();
                    using (var cancellation = sp.GetRequiredService<INotificationBuilder>().AddTitle(nameof(IsScheduledAsync)).Build().ScheduleTo(DateTimeOffset.Now + TimeSpan.FromDays(1)))
                    {
                        toastId = cancellation.ToastId;
                        Assert.True(await history.IsScheduledAsync(toastId), "scheduled not found");
                        Assert.False(await history.IsScheduledAsync(randomId), "random id found");
                    }
                    Assert.False(await history.IsScheduledAsync(toastId), "removed scheduled found");
                    Assert.False(await history.IsScheduledAsync(randomId), "random id found");
                }
            });

        [Fact]
        public Task RemoveScheduled()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                using (var sp = CreateServices())
                {
                    var history = sp.GetRequiredService<IHistory>();
                    var randomId = GetRandomScheduledToastId();
                    Assert.False(await history.IsScheduledAsync(randomId), "random id found");
                    await sp.GetRequiredService<IInitialization>().InitializeAsync();
                    var cancellation = sp.GetRequiredService<INotificationBuilder>().AddTitle(nameof(RemoveScheduled)).Build().ScheduleTo(DateTimeOffset.Now + TimeSpan.FromDays(1));
                    Assert.True(await history.IsScheduledAsync(cancellation.ToastId), "scheduled not found");
                    Assert.False(await history.IsScheduledAsync(randomId), "random id found");
                    history.RemoveScheduled(cancellation.ToastId);
                    Assert.False(await history.IsScheduledAsync(cancellation.ToastId), "removed scheduled found");
                    Assert.False(await history.IsScheduledAsync(randomId), "random id found");
                }
            });

        static ServiceProvider CreateServices()
            => Platform.CreateServiceCollection().BuildServiceProvider();

        static ToastId GetRandomScheduledToastId()
        {
#if __ANDROID__
            return new ToastId((1, 2).GetHashCode(), Guid.NewGuid().ToString());
#elif __IOS__
            return new ToastId(Guid.NewGuid().ToString());
#elif NETFX_CORE || MAUI_WINDOWS
            return new ToastId(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
#else
            throw new PlatformNotSupportedException();
#endif
        }
    }
}

#endif
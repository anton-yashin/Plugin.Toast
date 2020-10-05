﻿using Microsoft.Extensions.DependencyInjection;
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
                using var sc = CreateServices();
                var nm = sc.GetService<INotificationManager>();
                var history = sc.GetService<IHistory>();

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
                using var sc = CreateServices();
                var nm = sc.GetService<INotificationManager>();
                var history = sc.GetService<IHistory>();
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
                using var sc = CreateServices();
                var nm = sc.GetService<INotificationManager>();
                var history = sc.GetService<IHistory>();

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


        [Fact]
        public Task IsScheduledAsync()
            => Platform.iOS_InvokeOnMainThreadAsync(async () =>
            {
                using (var sp = CreateServices())
                {
                    ToastId toastId;
                    var history = sp.GetService<IHistory>();
                    var randomId = GetRandomScheduledToastId();
                    Assert.False(await history.IsScheduledAsync(randomId), "random id found");
                    await sp.GetRequiredService<IInitialization>().InitializeAsync();
                    using (var cancellation = sp.GetService<IBuilder>().AddTitle(nameof(IsScheduledAsync)).Build().ScheduleTo(DateTimeOffset.Now + TimeSpan.FromDays(1)))
                    {
                        toastId = cancellation.ToastId;
                        Assert.True(await history.IsScheduledAsync(toastId), "scheduled not found");
                        Assert.False(await history.IsScheduledAsync(randomId), "random id found");
                    }
                    Assert.False(await history.IsScheduledAsync(toastId), "removed scheduled found");
                    Assert.False(await history.IsScheduledAsync(randomId), "random id found");
                }
            });

        static ServiceProvider CreateServices()
        {
            var sc = new ServiceCollection();
#if __ANDROID__
            sc.AddNotificationManager(Platform.Activity);
#else
            sc.AddNotificationManager();
#endif
            return sc.BuildServiceProvider();
        }

        static ToastId GetRandomScheduledToastId()
        {
#if __ANDROID__
            return new ToastId((1, 2).GetHashCode(), Guid.NewGuid().ToString());
#elif __IOS__
            return new ToastId(Guid.NewGuid().ToString());
#elif NETFX_CORE
            return new ToastId(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), ToastIdNotificationType.ScheduledToastNotification);
#else
#error platform not supported
#endif
        }
    }
}

﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using UIKit;
using Plugin.Toast.IOS;

namespace Plugin.Toast
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNotificationManager(this IServiceCollection @this)
            => AddNotificationManager(@this, new ToastOptions());

        public static IServiceCollection AddNotificationManager(this IServiceCollection @this, IToastOptions options)
        {
            _ = @this ?? throw new ArgumentNullException(nameof(@this));
            _ = options ?? throw new ArgumentNullException(nameof(options));
            @this.TryAddSingleton<IToastOptions>(_ => options);
            @this.TryAddSingleton<INotificationReceiver, NotificationReceiver>();
            @this.TryAddTransient(typeof(IBuilder), UIDevice.CurrentDevice.CheckSystemVersion(10, 0)
                ? typeof(NotificationBuilder) : typeof(LocalNotificationBuilder));
            @this.TryAddTransient<IIosNotificationExtension, NotificationBuilder>();
            @this.TryAddTransient<IIosLocalNotificationExtension, LocalNotificationBuilder>();
            @this.TryAddSingleton<IPermission, Permission>();
            @this.TryAddSingleton(typeof(IInitialization), _ => _.GetService(typeof(IPermission)));
            return @this.AddBase();
        }
    }
}
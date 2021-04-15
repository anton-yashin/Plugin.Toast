﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public static partial class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddBase(this IServiceCollection @this)
        {
            @this.TryAddSingleton<IInitialization, NoInitialization>();
            @this.TryAddSingleton<INotificationManager, DiNm>();
            @this.TryAddSingleton<ISystemEventSource, SystemEventSource>();
            @this.TryAddTransient<INotificationEventSource, NotificationEventProxy>();
            @this.TryAddTransient(typeof(IBuilder<>), typeof(BuilderFactory<>));
            @this.TryAddTransient(typeof(IBuilder<,>), typeof(BuilderFactory<,>));
            @this.TryAddTransient(typeof(IBuilder<,,>), typeof(BuilderFactory<,,>));
            @this.TryAddTransient(typeof(IBuilder<,,,>), typeof(BuilderFactory<,,,>));
            return @this;
        }
    }
}

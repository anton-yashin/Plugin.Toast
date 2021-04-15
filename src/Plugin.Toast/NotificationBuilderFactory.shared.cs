using Microsoft.Extensions.DependencyInjection;
using Plugin.Toast.Abstractions;
using System;
using System.Linq;

namespace Plugin.Toast
{
    abstract class NotificationBuilderFactory : INotificationBuilder
    {
        private readonly INotificationBuilder builder;

        protected NotificationBuilderFactory(IServiceProvider serviceProvider, Type t1)
        {
            _ = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            builder = (INotificationBuilder)(serviceProvider.GetService(t1) ?? serviceProvider.GetRequiredService(typeof(INotificationBuilder)));
        }

        protected NotificationBuilderFactory(IServiceProvider serviceProvider, Type t1, Type t2)
        {
            _ = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            builder = (INotificationBuilder)(serviceProvider.GetService(t1)
                ?? serviceProvider.GetService(t2)
                ?? serviceProvider.GetRequiredService(typeof(INotificationBuilder)));
        }

        protected NotificationBuilderFactory(IServiceProvider serviceProvider, params Type[] types)
        {
            _ = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            builder = (from i in types
                       let j = serviceProvider.GetService(i) as INotificationBuilder
                       where j != null
                       select j).FirstOrDefault()
                  ?? (INotificationBuilder)serviceProvider.GetRequiredService(typeof(INotificationBuilder));
        }

        INotificationBuilder INotificationBuilder.AddDescription(string description) => builder.AddDescription(description);
        INotificationBuilder INotificationBuilder.AddTitle(string title) => builder.AddTitle(title);
        INotification IBuilder.Build() => builder.Build();
        IBuilder IBuilder.WhenUsing<T>(Action<T> buildAction) => builder.WhenUsing(buildAction);
        IBuilder IBuilder.UseConfiguration<T>(T token) => builder.UseConfiguration(token);
        IBuilder IBuilder.Add<T1>(T1 a1)
            => builder.Add(a1);
        IBuilder IBuilder.Add<T1, T2>(T1 a1, T2 a2)
            => builder.Add(a1, a2);
        IBuilder IBuilder.Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3)
            => builder.Add(a1, a2, a3);
        IBuilder IBuilder.Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
            => builder.Add(a1, a2, a3, a4);
        IBuilder IBuilder.Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
            => builder.Add(a1, a2, a3, a4, a5);
        IBuilder IBuilder.Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
            => builder.Add(a1, a2, a3, a4, a5, a6);
        IBuilder IBuilder.Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
            => builder.Add(a1, a2, a3, a4, a5, a6, a7);
        IBuilder IBuilder.Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
            => builder.Add(a1, a2, a3, a4, a5, a6, a7, a8);
        IBuilder IBuilder.Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
            => builder.Add(a1, a2, a3, a4, a5, a6, a7, a8, a9);
    }

    sealed class NotificationBuilderFactory<T1> : NotificationBuilderFactory, INotificationBuilder<T1>
        where T1 : INotificationBuilderExtension<T1>
    {
        public NotificationBuilderFactory(IServiceProvider serviceProvider)
            : base(serviceProvider, typeof(T1))
        { }
    }

    sealed class NotificationBuilderFactory<T1, T2> : NotificationBuilderFactory, INotificationBuilder<T1, T2>
        where T1 : INotificationBuilderExtension<T1>
        where T2 : INotificationBuilderExtension<T2>
    {
        public NotificationBuilderFactory(IServiceProvider serviceProvider)
            : base(serviceProvider, typeof(T1), typeof(T2))
        { }
    }

    sealed class NotificationBuilderFactory<T1, T2, T3> : NotificationBuilderFactory, INotificationBuilder<T1, T2, T3>
        where T1 : INotificationBuilderExtension<T1>
        where T2 : INotificationBuilderExtension<T2>
        where T3 : INotificationBuilderExtension<T3>
    {
        public NotificationBuilderFactory(IServiceProvider serviceProvider)
            : base(serviceProvider, typeof(T1), typeof(T2), typeof(T3))
        { }
    }

    sealed class NotificationBuilderFactory<T1, T2, T3, T4> : NotificationBuilderFactory, INotificationBuilder<T1, T2, T3, T4>
        where T1 : INotificationBuilderExtension<T1>
        where T2 : INotificationBuilderExtension<T2>
        where T3 : INotificationBuilderExtension<T3>
        where T4 : INotificationBuilderExtension<T4>
    {
        public NotificationBuilderFactory(IServiceProvider serviceProvider)
            : base(serviceProvider, typeof(T1), typeof(T2), typeof(T3), typeof(T4))
        { }
    }

}

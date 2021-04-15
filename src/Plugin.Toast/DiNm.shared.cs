using Microsoft.Extensions.DependencyInjection;
using Plugin.Toast.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    sealed class DiNm : INotificationManager
    {
        private readonly IServiceProvider serviceProvider;

        public DiNm(IServiceProvider serviceProvider) => this.serviceProvider = serviceProvider;

        public Task InitializeAsync() => ((IInitialization)serviceProvider.GetService(typeof(IInitialization))).InitializeAsync();

        public INotificationBuilder GetBuilder() => (INotificationBuilder)serviceProvider.GetRequiredService(typeof(INotificationBuilder));

        public INotificationBuilder GetBuilder<T>()
            where T : INotificationBuilderExtension<T>
            => (INotificationBuilder)serviceProvider.GetService(typeof(T)) 
            ?? GetBuilder();

        public INotificationBuilder GetBuilder<T1, T2>()
            where T1 : INotificationBuilderExtension<T1>
            where T2 : INotificationBuilderExtension<T2>
            => (INotificationBuilder)(serviceProvider.GetService(typeof(T1))
            ?? serviceProvider.GetService(typeof(T2))) 
            ?? GetBuilder();

        public INotificationBuilder GetBuilder<T1, T2, T3>()
            where T1 : INotificationBuilderExtension<T1>
            where T2 : INotificationBuilderExtension<T2>
            where T3 : INotificationBuilderExtension<T3>
            => BuildNotificationUsing(typeof(T1), typeof(T2), typeof(T3));

        public INotificationBuilder GetBuilder<T1, T2, T3, T4>()
            where T1 : INotificationBuilderExtension<T1>
            where T2 : INotificationBuilderExtension<T2>
            where T3 : INotificationBuilderExtension<T3>
            where T4 : INotificationBuilderExtension<T4>
            => BuildNotificationUsing(typeof(T1), typeof(T2), typeof(T3), typeof(T4));

        INotificationBuilder BuildNotificationUsing(params Type[] types)
            => (from i in types
                let j = serviceProvider.GetService(i) as INotificationBuilder
                where j != null
                select j).FirstOrDefault()
            ?? GetBuilder();
    }
}

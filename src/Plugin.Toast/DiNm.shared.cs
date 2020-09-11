using Microsoft.Extensions.DependencyInjection;
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

        public IBuilder BuildNotification() => (IBuilder)serviceProvider.GetRequiredService(typeof(IBuilder));

        public IBuilder BuildNotificationUsing<T>()
            where T : IBuilderExtension<T>
            => (IBuilder)serviceProvider.GetService(typeof(T)) 
            ?? BuildNotification();

        public IBuilder BuildNotificationUsing<T1, T2>()
            where T1 : IBuilderExtension<T1>
            where T2 : IBuilderExtension<T2>
            => (IBuilder)(serviceProvider.GetService(typeof(T1))
            ?? serviceProvider.GetService(typeof(T2))) 
            ?? BuildNotification();

        public IBuilder BuildNotificationUsing<T1, T2, T3>()
            where T1 : IBuilderExtension<T1>
            where T2 : IBuilderExtension<T2>
            where T3 : IBuilderExtension<T3>
            => BuildNotificationUsing(typeof(T1), typeof(T2), typeof(T3));

        public IBuilder BuildNotificationUsing<T1, T2, T3, T4>()
            where T1 : IBuilderExtension<T1>
            where T2 : IBuilderExtension<T2>
            where T3 : IBuilderExtension<T3>
            where T4 : IBuilderExtension<T4>
            => BuildNotificationUsing(typeof(T1), typeof(T2), typeof(T3), typeof(T4));

        IBuilder BuildNotificationUsing(params Type[] types)
            => (from i in types
                let j = serviceProvider.GetService(i) as IBuilder
                where j != null
                select j).FirstOrDefault()
            ?? BuildNotification();
    }
}

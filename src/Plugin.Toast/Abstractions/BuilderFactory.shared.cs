using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Plugin.Toast.Abstractions
{
    abstract class BuilderFactory : IBuilder
    {
        private readonly IBuilder builder;

        protected BuilderFactory(IServiceProvider serviceProvider, Type t1)
        {
            _ = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            builder = (IBuilder)(serviceProvider.GetService(t1) ?? serviceProvider.GetRequiredService(typeof(IBuilder)));
        }

        protected BuilderFactory(IServiceProvider serviceProvider, Type t1, Type t2)
        {
            _ = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            builder = (IBuilder)(serviceProvider.GetService(t1)
                ?? serviceProvider.GetService(t2)
                ?? serviceProvider.GetRequiredService(typeof(IBuilder)));
        }

        protected BuilderFactory(IServiceProvider serviceProvider, params Type[] types)
        {
            _ = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            builder = (from i in types
                       let j = serviceProvider.GetService(i) as IBuilder
                       where j != null
                       select j).FirstOrDefault()
                  ?? (IBuilder)serviceProvider.GetRequiredService(typeof(IBuilder));
        }

        IBuilder IBuilder.AddDescription(string description) => builder.AddDescription(description);
        IBuilder IBuilder.AddTitle(string title) => builder.AddTitle(title);
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

    sealed class BuilderFactory<T1> : BuilderFactory, IBuilder<T1>
        where T1 : IBuilderExtension<T1>
    {
        public BuilderFactory(IServiceProvider serviceProvider)
            : base(serviceProvider, typeof(T1))
        { }
    }

    sealed class BuilderFactory<T1, T2> : BuilderFactory, IBuilder<T1, T2>
        where T1 : IBuilderExtension<T1>
        where T2 : IBuilderExtension<T2>
    {
        public BuilderFactory(IServiceProvider serviceProvider)
            : base(serviceProvider, typeof(T1), typeof(T2))
        { }
    }

    sealed class BuilderFactory<T1, T2, T3> : BuilderFactory, IBuilder<T1, T2, T3>
        where T1 : IBuilderExtension<T1>
        where T2 : IBuilderExtension<T2>
        where T3 : IBuilderExtension<T3>
    {
        public BuilderFactory(IServiceProvider serviceProvider)
            : base(serviceProvider, typeof(T1), typeof(T2), typeof(T3))
        { }
    }

    sealed class BuilderFactory<T1, T2, T3, T4> : BuilderFactory, IBuilder<T1, T2, T3, T4>
        where T1 : IBuilderExtension<T1>
        where T2 : IBuilderExtension<T2>
        where T3 : IBuilderExtension<T3>
        where T4 : IBuilderExtension<T4>
    {
        public BuilderFactory(IServiceProvider serviceProvider)
            : base(serviceProvider, typeof(T1), typeof(T2), typeof(T3), typeof(T4))
        { }
    }

}

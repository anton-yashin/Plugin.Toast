using LightMock;
using Plugin.Toast;
using System;

namespace UnitTests.Mocks
{
    public sealed class MockBulider : IBuilder
    {
        private readonly IInvocationContext<IBuilder> context;

        public MockBulider(IInvocationContext<IBuilder> context) => this.context = context;

        public IBuilder Add<T1>(T1 a1)
            => context.Invoke(_ => _.Add(a1));

        public IBuilder Add<T1, T2>(T1 a1, T2 a2)
            => context.Invoke(_ => _.Add(a1, a2));

        public IBuilder Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3)
            => context.Invoke(_ => _.Add(a1, a2, a3));

        public IBuilder Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4));

        public IBuilder Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5));

        public IBuilder Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6));

        public IBuilder Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6, a7));

        public IBuilder Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6, a7, a8));

        public IBuilder Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6, a7, a8, a9));

        public IBuilder AddDescription(string description)
            => context.Invoke(_ => _.AddDescription(description));

        public IBuilder AddTitle(string title)
            => context.Invoke(_ => _.AddTitle(title));

        public INotification Build()
            => context.Invoke(_ => _.Build());

        public IBuilder UseConfiguration<T>(T token)
            => context.Invoke(_ => _.UseConfiguration(token));

        public IBuilder WhenUsing<T>(Action<T> buildAction) where T : IBuilderExtension<T>
            => context.Invoke(_ => _.WhenUsing(buildAction));
    }
}

using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Hosting;
using System;

namespace ManualTests.Maui.Services
{
    public interface ICoreDispatcher : IDispatcher, IMauiInitializeService { }

    sealed class CoreDispatcher : ICoreDispatcher
    {
        private readonly IDispatcher dispatcher;

        public CoreDispatcher(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        public bool IsDispatchRequired => dispatcher.IsDispatchRequired;

        public IDispatcherTimer CreateTimer() => dispatcher.CreateTimer();

        public bool Dispatch(Action action) => dispatcher.Dispatch(action);

        public bool DispatchDelayed(TimeSpan delay, Action action)
            => dispatcher.DispatchDelayed(delay, action);

        public void Initialize(IServiceProvider services)
        {
            // Do nothing.
        }
    }
}

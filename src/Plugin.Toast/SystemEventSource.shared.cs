using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    sealed class SystemEventSource : ISystemEventSource
    {
        private readonly ILogger<SystemEventSource>? logger;
        private readonly List<WeakReference<INotificationEventObserver>> observers;
        private readonly object @lock;

        public SystemEventSource(IServiceProvider? serviceProvider)
        {
            this.logger = serviceProvider?.GetService<ILogger<SystemEventSource>>();
            observers = new List<WeakReference<INotificationEventObserver>>();
            @lock = new object();
            //
            Platform.SystemEventSource = this;
        }

        public void SendEvent(NotificationEvent @event)
        {
            var observers = GetObservers();
            SendEvent(observers, @event);
        }

        public void SendPendingEvents()
        {
            var events = Platform.PendingEvents;
            Platform.ClearPendingEvents();
            var observers = GetObservers();
            foreach (var e in events)
                SendEvent(observers, e);
        }

        public void Subscribe(INotificationEventObserver observer)
        {
            lock (@lock)
            {
                observers.Add(new WeakReference<INotificationEventObserver>(observer));
            }
        }

        public void Unsubscribe(INotificationEventObserver observer)
        {
            lock (@lock)
            {
                for (int i = 0; i < observers.Count; i++)
                {
                    if (observers[i].TryGetTarget(out var tgt) == false)
                    {
                        observers.RemoveAt(i--);
                        continue;
                    }
                    if (ReferenceEquals(tgt, observer))
                        observers.RemoveAt(i--);
                }
            }
        }

        IReadOnlyCollection<INotificationEventObserver> GetObservers()
        {
            var result = new List<INotificationEventObserver>();
            lock (@lock)
            {
                for (int i = 0; i < observers.Count; i++)
                {
                    if (observers[i].TryGetTarget(out var tgt) == false)
                    {
                        observers.RemoveAt(i--);
                        continue;
                    }
                    result.Add(tgt);
                }
            }
            return result;
        }

        void SendEvent(IEnumerable<INotificationEventObserver> observers, NotificationEvent @event)
        {
            try
            {
                foreach (var i in observers)
                    i.OnNotificationReceived(@event);
            }
            catch (Exception ex)
            {
#if NETSTANDARD1_4 == false
                logger?.LogError(ex, "observer error");
#endif
                throw;
            }
        }
    }
}

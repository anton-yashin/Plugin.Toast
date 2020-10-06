﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    sealed class SystemEventSource : ISystemEventSource
    {
        List<WeakReference<INotificationEventObserver>> observers;
        object @lock;

        public SystemEventSource()
        {
            observers = new List<WeakReference<INotificationEventObserver>>();
            @lock = new object();
            //
            Platform.SystemEventRouter = this;
        }

        public void SendEvent(NotificationEvent @event)
        {
            var observers = GetObservers();
            SendEvent(observers, @event);
        }

        public void SendPendingEvents()
        {
            var events = Platform.PendintEvents;
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
            foreach (var i in observers)
                i.OnNotificationReceived(@event);
        }
    }
}
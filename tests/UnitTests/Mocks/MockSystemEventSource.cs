using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Mocks
{
    sealed class MockSystemEventSource : ISystemEventSource
    {
        public int SendEventCallCount { get; private set; }
        public Action<NotificationEvent>? OnSendEvent = null;

        public void SendEvent(NotificationEvent @event)
        {
            SendEventCallCount++;
            OnSendEvent?.Invoke(@event);
        }

        public int SendPendingEventsCallCount { get; private set; }
        public Action? OnSendPendingEvents = null;

        public void SendPendingEvents()
        {
            SendPendingEventsCallCount++;
            OnSendPendingEvents?.Invoke();
        }

        public int SubscribeCallCount { get; private set; }
        public Action<INotificationEventObserver>? OnSubscribe = null;

        public void Subscribe(INotificationEventObserver observer)
        {
            SubscribeCallCount++;
            OnSubscribe?.Invoke(observer);
        }

        public int UnsubscribeCallCount { get; private set; }
        public Action<INotificationEventObserver>? OnUnsubscribe = null;

        public void Unsubscribe(INotificationEventObserver observer)
        {
            UnsubscribeCallCount++;
            OnUnsubscribe?.Invoke(observer);
        }
    }
}

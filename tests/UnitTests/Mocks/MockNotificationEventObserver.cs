using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Mocks
{
    sealed class MockNotificationEventObserver : INotificationEventObserver
    {
        public int OnNotificationReceivedCallCount { get; private set; }
        public Action<NotificationEvent>? OnOnNotificationReceived = null;

        public void OnNotificationReceived(NotificationEvent @event)
        {
            OnNotificationReceivedCallCount++;
            OnOnNotificationReceived?.Invoke(@event);
        }
    }
}

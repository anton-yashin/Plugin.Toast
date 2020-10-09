using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests
{
    public class NotificationEventProxy_Tests
    {
        [Fact]
        public void Subscribe()
        {
            // prepare
            INotificationEventObserver result = null!;
            var mock = new MockSystemEventSource();
            mock.OnSubscribe += o => result = o;

            // act
            var expected = new NotificationEventProxy(mock);

            // verify
            Assert.Same(expected, result);
        }

        [Fact]
        public void NotificationReceived()
        {
            // prepare
            bool notificationReceivedIsCalled = false;
            var expected = new NotificationEvent(null!);
            var mock = new MockSystemEventSource();
            var proxy = new NotificationEventProxy(mock);
            proxy.NotificationReceived += Verify;

            // act
            proxy.OnNotificationReceived(expected);

            // verify
            Assert.True(notificationReceivedIsCalled);
            void Verify(object? sender, NotificationEvent e)
            {
                notificationReceivedIsCalled = true;
                Assert.Same(expected: mock, sender);
                Assert.Same(expected, e);
            }
        }

        [Fact]
        public void SendPendingEvents()
        {
            // prepare
            var mock = new MockSystemEventSource();
            var proxy = new NotificationEventProxy(mock);
            Assert.Equal(0, mock.SendPendingEventsCallCount);

            // act
            proxy.SendPendingEvents();

            // verify
            Assert.Equal(1, mock.SendPendingEventsCallCount);
        }
    }
}

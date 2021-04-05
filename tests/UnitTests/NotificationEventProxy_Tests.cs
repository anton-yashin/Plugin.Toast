using LightMock;
using LightMock.Generator;
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
            var mock = new Mock<ISystemEventSource>();
            mock.Arrange(f => f.Subscribe(The<INotificationEventObserver>.IsAnyValue));

            // act
            var expected = new NotificationEventProxy(mock.Object);

            // verify
            mock.Assert(f => f.Subscribe(The<INotificationEventObserver>.Is(neo => ReferenceEquals(neo, expected))));
        }

        [Fact]
        public void NotificationReceived()
        {
            // prepare
            bool notificationReceivedIsCalled = false;
            var expected = new NotificationEvent(null!);
            var mock = new Mock<ISystemEventSource>();
            var proxy = new NotificationEventProxy(mock.Object);
            proxy.NotificationReceived += Verify;

            // act
            proxy.OnNotificationReceived(expected);

            // verify
            Assert.True(notificationReceivedIsCalled);
            void Verify(object sender, NotificationEvent e)
            {
                notificationReceivedIsCalled = true;
                Assert.Same(expected: mock.Object, sender);
                Assert.Same(expected, e);
            }
        }

        [Fact]
        public void SendPendingEvents()
        {
            // prepare
            var mock = new Mock<ISystemEventSource>();
            var proxy = new NotificationEventProxy(mock.Object);

            // act
            proxy.SendPendingEvents();

            // verify
            mock.Assert(f => f.SendPendingEvents(), Invoked.Once);
        }
    }
}

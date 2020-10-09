using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests
{
    public class SystemEventSource_Tests
    {
        [Fact]
        public void SubscribeAndSendEvent()
        {
            // prepare
            var expected = new NotificationEvent(null!);
            var mock = new MockNotificationEventObserver();
            mock.OnOnNotificationReceived += Verify;
            var ses = new SystemEventSource(null);

            // act
            ses.Subscribe(mock);
            ses.SendEvent(expected);

            // verify
            Assert.Equal(1, mock.OnNotificationReceivedCallCount);
            Assert.Equal(1, ses.GetObserversCount());
            void Verify(NotificationEvent result) => Assert.Same(expected, result);
        }

        [Fact]
        public void SusbscribeAndUnsubscribve()
        {
            // prepare
            var expected = new NotificationEvent(null!);
            var mock = new MockNotificationEventObserver();
            var ses = new SystemEventSource(null);

            // act
            ses.Subscribe(mock);
            ses.Unsubscribe(mock);
            ses.SendEvent(expected);

            // verify
            Assert.Equal(0, mock.OnNotificationReceivedCallCount);
            Assert.Equal(0, ses.GetObserversCount());
        }

        [Fact]
        public void SusbscribeAndGC()
        {
            // prepare
            var expected = new NotificationEvent(null!);
            var ses = new SystemEventSource(null);
            var wr = RegisterSomeWeak(ses);
            Assert.Equal(1, ses.GetObserversCount());

            // act
            GC.Collect();
            GC.WaitForPendingFinalizers();
            ses.SendEvent(expected);

            // verify
            Assert.Equal(0, ses.GetObserversCount());
            Assert.Null(wr.Target);
        }

        [Fact]
        public void UnsubscribeAndGC()
        {
            // prepare
            var ses = new SystemEventSource(null);
            var wr = RegisterSomeWeak(ses);
            Assert.Equal(1, ses.GetObserversCount());

            // act
            GC.Collect();
            GC.WaitForPendingFinalizers();
            ses.Unsubscribe(null!);

            // verify
            Assert.Equal(0, ses.GetObserversCount());
            Assert.Null(wr.Target);
        }

        static WeakReference RegisterSomeWeak(SystemEventSource ses)
        {
            var mock = new MockNotificationEventObserver();
            ses.Subscribe(mock);
            return new WeakReference(mock, true);
        }

        [Fact]
        public void PlatformSendPendingEvents()
        {
            // prepare
            var expected = new NotificationEvent(null!);
            Platform.AddPendingEvent(expected);
            var ses = new SystemEventSource(null);
            var mock = new MockNotificationEventObserver();
            mock.OnOnNotificationReceived += Verify;
            ses.Subscribe(mock);

            // act
            ses.SendPendingEvents();

            Assert.Same(expected: ses, Platform.SystemEventSource);
            Assert.Equal(1, mock.OnNotificationReceivedCallCount);
            void Verify(NotificationEvent result) => Assert.Same(expected, result);
        }

        [Fact]
        public void NoPendingEvents()
        {
            Assert.Empty(Platform.PendingEvents);
        }

        [Fact]
        public void AddPendingEventThenClear()
        {
            // prepare
            var expected = new NotificationEvent(null!);

            // act & verify
            Platform.AddPendingEvent(expected);
            Assert.Contains(expected, Platform.PendingEvents);
            Platform.ClearPendingEvents();
            Assert.Empty(Platform.PendingEvents);
        }
    }
}

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
    public class SystemEventSource_Tests
    {
        [Fact]
        public void SubscribeAndSendEvent()
        {
            lock (Platform.Lock)
            {
                // prepare
                var expected = new NotificationEvent(null!);
                var mock = new Mock<INotificationEventObserver>();
                var ses = new SystemEventSource(null);

                // act
                ses.Subscribe(mock.Object);
                ses.SendEvent(expected);

                // verify
                Assert.Equal(1, ses.GetObserversCount());
                mock.Assert(f => f.OnNotificationReceived(
                    The<NotificationEvent>.Is(ne => ReferenceEquals(expected, ne))));
            }
        }

        [Fact]
        public void SusbscribeAndUnsubscribve()
        {
            lock (Platform.Lock)
            {
                // prepare
                var expected = new NotificationEvent(null!);
                var mock = new Mock<INotificationEventObserver>();
                var ses = new SystemEventSource(null);

                // act
                ses.Subscribe(mock.Object);
                ses.Unsubscribe(mock.Object);
                ses.SendEvent(expected);

                // verify
                mock.AssertNoOtherCalls();
                Assert.Equal(0, ses.GetObserversCount());
            }
        }

#if (DEBUG && __IPHONE__) == false

        [Fact]
        public void SusbscribeAndGC()
        {
            lock (Platform.Lock)
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
        }

        [Fact]
        public void UnsubscribeAndGC()
        {
            lock (Platform.Lock)
            {
                // prepare
                var ses = new SystemEventSource(null);
                var wr = RegisterSomeWeak(ses);
                Assert.Equal(1, ses.GetObserversCount());

                // act
                var generation = GC.GetGeneration(wr);
                GC.Collect(generation, GCCollectionMode.Forced, blocking: true, compacting: true);
                GC.WaitForPendingFinalizers();
                ses.Unsubscribe(null!);

                // verify
                Assert.Equal(0, ses.GetObserversCount());
                Assert.Null(wr.Target);
            }
        }

        static WeakReference RegisterSomeWeak(SystemEventSource ses)
        {
            var mock = new Mock<INotificationEventObserver>();
            ses.Subscribe(mock.Object);
            return new WeakReference(mock, true);
        }

#endif

        [Fact]
        public void PlatformSendPendingEvents()
        {
            // prepare
            lock (Platform.Lock)
            {
                var expected = new NotificationEvent(null!);
                Platform.AddPendingEvent(expected);
                var ses = new SystemEventSource(null);
                var mock = new Mock<INotificationEventObserver>();
                ses.Subscribe(mock.Object);

                // act
                ses.SendPendingEvents();

                Assert.Same(expected: ses, Platform.SystemEventSource);
                mock.Assert(f => f.OnNotificationReceived(
                    The<NotificationEvent>.Is(result => ReferenceEquals(expected, result))), Invoked.Once);
            }
        }

        [Fact]
        public void NoPendingEvents()
        {
            lock (Platform.Lock)
            {
                Assert.Empty(Platform.PendingEvents);
            }
        }

        [Fact]
        public void AddPendingEventThenClear()
        {
            lock (Platform.Lock)
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
}

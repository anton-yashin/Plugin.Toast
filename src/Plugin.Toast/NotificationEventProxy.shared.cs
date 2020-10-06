using System;

namespace Plugin.Toast
{
    sealed class NotificationEventProxy : INotificationEventObserver, INotificationEventSource
    {
        private readonly ISystemEventSource systemEventRouter;

        public NotificationEventProxy(ISystemEventSource systemEventRouter)
        {
            this.systemEventRouter = systemEventRouter;
            systemEventRouter.Subscribe(this);
        }

        public event EventHandler<NotificationEvent>? NotificationReceived;

        public void OnNotificationReceived(NotificationEvent @event)
            => NotificationReceived?.Invoke(systemEventRouter, @event);

        public void SendPendingEvents() => systemEventRouter.SendPendingEvents();
    }
}

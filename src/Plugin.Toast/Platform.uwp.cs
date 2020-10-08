using Plugin.Toast.UWP;
using System;
using System.Web;
using Windows.ApplicationModel.Activation;

namespace Plugin.Toast
{
    public static partial class Platform
    {
        /// <summary>
        /// You must call this function if you want to receive an event received at application startup.
        /// Place call inside App.OnActivated function.
        /// </summary>
        /// <param name="args">Activation args</param>
        public static void OnActivated(ToastNotificationActivatedEventArgs? args)
        {
            if (args != null)
            {
                var collection = HttpUtility.ParseQueryString(args.Argument);
                var tag = collection[UwpConstants.KTag];
                var group = collection[UwpConstants.KGroup];
                if (tag == null)
                    throw new InvalidOperationException("Tag value isn't found");
                var tid = new ToastId(tag, group);
                var @event = new NotificationEvent(tid);
                var activator = SystemEventSource;
                if (activator == null)
                    AddPendingEvent(@event);
                else
                    activator.SendEvent(@event);
            }
        }

    }
}

using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface IIosLocalNotificationExtension : INotificationBuilderExtension<IIosLocalNotificationExtension>
    {
        IIosLocalNotificationExtension AddSoundName(string soundName);
        IIosLocalNotificationExtension WithCustomArg(string key, string value);
        IIosLocalNotificationExtension WithCustomArgs(IEnumerable<(string key, string value)> args);
    }
}

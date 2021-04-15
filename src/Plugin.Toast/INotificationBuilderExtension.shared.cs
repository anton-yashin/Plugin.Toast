using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface INotificationBuilderExtension<T> : IBuilderExtension<T>
        where T : INotificationBuilderExtension<T>
    {
        T AddTitle(string title);
        T AddDescription(string description);
    }
}

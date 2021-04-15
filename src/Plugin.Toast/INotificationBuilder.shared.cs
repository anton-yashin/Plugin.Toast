using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface INotificationBuilder : IBuilder
    {
        INotificationBuilder AddTitle(string title);
        INotificationBuilder AddDescription(string description);
    }

    public interface INotificationBuilder<T> : INotificationBuilder
        where T : INotificationBuilderExtension<T>
    { }

    public interface INotificationBuilder<T1, T2> : INotificationBuilder
        where T1 : INotificationBuilderExtension<T1>
        where T2 : INotificationBuilderExtension<T2>
    { }

    public interface INotificationBuilder<T1, T2, T3> : INotificationBuilder
        where T1 : INotificationBuilderExtension<T1>
        where T2 : INotificationBuilderExtension<T2>
        where T3 : INotificationBuilderExtension<T3>
    { }

    public interface INotificationBuilder<T1, T2, T3, T4> : IBuilder
        where T1 : INotificationBuilderExtension<T1>
        where T2 : INotificationBuilderExtension<T2>
        where T3 : INotificationBuilderExtension<T3>
        where T4 : INotificationBuilderExtension<T4>
    { }

}

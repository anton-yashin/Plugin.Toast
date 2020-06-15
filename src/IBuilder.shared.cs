using System;

namespace Plugin.Toast
{
    public interface IBuilder
    {
        IBuilder AddTitle(string title);
        IBuilder AddDescription(string description);
        /// <summary>
        /// add platform specific options
        /// <seealso cref="IDroidNotificationExtension"/>,
        /// <seealso cref="ISnackbarExtension"/>,
        /// <seealso cref="IIosLocalNotificationExtension"/>,
        /// <seealso cref="IUwpExtension"/>
        /// </summary>
        IBuilder WhenUsing<T>(Action<T> buildAction) where T : IBuilderExtension<T>;

        /// <summary>
        /// Builds a notification.
        /// </summary>
        /// <returns>notification</returns>
        /// <exception cref="InvalidOperationException">
        /// If this method used more than once
        /// </exception>
        INotification Build();

        IBuilder UseConfiguration<T>(T token);
    }

    public interface IBuilder<T> : IBuilder
        where T : IBuilderExtension<T>
    { }

    public interface IBuilder<T1, T2> : IBuilder
        where T1 : IBuilderExtension<T1>
        where T2 : IBuilderExtension<T2>
    { }

    public interface IBuilder<T1, T2, T3> : IBuilder
        where T1 : IBuilderExtension<T1>
        where T2 : IBuilderExtension<T2>
        where T3 : IBuilderExtension<T3>
    { }

    public interface IBuilder<T1, T2, T3, T4> : IBuilder
        where T1 : IBuilderExtension<T1>
        where T2 : IBuilderExtension<T2>
        where T3 : IBuilderExtension<T3>
        where T4 : IBuilderExtension<T4>
    { }
}

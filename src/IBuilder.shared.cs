using System;

namespace Plugin.Toast
{
    public interface IBuilder
    {
        IBuilder AddTitle(string title);
        IBuilder AddDescription(string description);
        IBuilder Add<T1>(T1 a1);
        IBuilder Add<T1, T2>(T1 a1, T2 a2);
        IBuilder Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3);
        IBuilder Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4);
        IBuilder Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5);
        IBuilder Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6);
        IBuilder Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7);
        IBuilder Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8);
        IBuilder Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9);
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

using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast.Abstractions
{
    public interface IBuilderExtension<T>
        where T: IBuilderExtension<T>
    {
        T Use(IExtensionConfiguration<T> visitor);
        T AddTitle(string title);
        T AddDescription(string description);
        /// <summary>
        /// This function will search an implementation of <see cref="IExtensionPlugin{TExtension, T1}"/> in IoC and
        /// call <see cref="IExtensionPlugin{TExtension, T1}.Configure(TExtension, T1)"/>.
        /// If implementation not found in IoC then it do nothing. If found several implementations then it will call
        /// <see cref="IExtensionPlugin{TExtension, T1}.Configure(TExtension, T1)"/> at each.
        /// <seealso cref="IDroidNotificationExtension"/>,
        /// <seealso cref="ISnackbarExtension"/>,
        /// <seealso cref="IIosLocalNotificationExtension"/>,
        /// <seealso cref="IIosNotificationExtension"/>
        /// <seealso cref="IUwpExtension"/>
        /// </summary>
        /// <typeparam name="T1">A type to forward.</typeparam>
        /// <param name="a1">A data to forward.</param>
        /// <returns>Instance of current builder</returns>
        T Add<T1>(T1 a1);
        /// <summary>
        /// This function will search an implementation of <see cref="IExtensionPlugin{TExtension, T1, T2}"/> in IoC and
        /// call <see cref="IExtensionPlugin{TExtension, T1, T2}.Configure(TExtension, T1, T2)"/>.
        /// If implementation not found in IoC then it do nothing. If found several implementations then it will call
        /// <see cref="IExtensionPlugin{TExtension, T1, T2}.Configure(TExtension, T1, T2)"/> at each.
        /// <seealso cref="IDroidNotificationExtension"/>,
        /// <seealso cref="ISnackbarExtension"/>,
        /// <seealso cref="IIosLocalNotificationExtension"/>,
        /// <seealso cref="IIosNotificationExtension"/>
        /// <seealso cref="IUwpExtension"/>
        /// </summary>
        /// <typeparam name="T1">A type to forward.</typeparam>
        /// <typeparam name="T2">A type to forward.</typeparam>
        /// <param name="a1">A data to forward.</param>
        /// <param name="a2">A data to forward.</param>
        /// <returns>Instance of current builder</returns>
        T Add<T1, T2>(T1 a1, T2 a2);
        /// <summary>
        /// This function will search an implementation of <see cref="IExtensionPlugin{TExtension, T1, T2, T3}"/> in IoC and
        /// call <see cref="IExtensionPlugin{TExtension, T1, T2, T3}.Configure(TExtension, T1, T2, T3)"/>.
        /// If implementation not found in IoC then it do nothing. If found several implementations then it will call
        /// <see cref="IExtensionPlugin{TExtension, T1, T2, T3}.Configure(TExtension, T1, T2, T3)"/> at each.
        /// <seealso cref="IDroidNotificationExtension"/>,
        /// <seealso cref="ISnackbarExtension"/>,
        /// <seealso cref="IIosLocalNotificationExtension"/>,
        /// <seealso cref="IIosNotificationExtension"/>
        /// <seealso cref="IUwpExtension"/>
        /// </summary>
        /// <typeparam name="T1">A type to forward.</typeparam>
        /// <typeparam name="T2">A type to forward.</typeparam>
        /// <typeparam name="T3">A type to forward.</typeparam>
        /// <param name="a1">A data to forward.</param>
        /// <param name="a2">A data to forward.</param>
        /// <param name="a3">A data to forward.</param>
        /// <returns>Instance of current builder</returns>
        T Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3);
        /// <summary>
        /// This function will search an implementation of <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4}"/> in IoC and
        /// call <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4}.Configure(TExtension, T1, T2, T3, T4)"/>.
        /// If implementation not found in IoC then it do nothing. If found several implementations then it will call
        /// <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4}.Configure(TExtension, T1, T2, T3, T4)"/> at each.
        /// <seealso cref="IDroidNotificationExtension"/>,
        /// <seealso cref="ISnackbarExtension"/>,
        /// <seealso cref="IIosLocalNotificationExtension"/>,
        /// <seealso cref="IIosNotificationExtension"/>
        /// <seealso cref="IUwpExtension"/>
        /// </summary>
        /// <typeparam name="T1">A type to forward.</typeparam>
        /// <typeparam name="T2">A type to forward.</typeparam>
        /// <typeparam name="T3">A type to forward.</typeparam>
        /// <typeparam name="T4">A type to forward.</typeparam>
        /// <param name="a1">A data to forward.</param>
        /// <param name="a2">A data to forward.</param>
        /// <param name="a3">A data to forward.</param>
        /// <param name="a4">A data to forward.</param>
        /// <returns>Instance of current builder</returns>
        T Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4);
        /// <summary>
        /// This function will search an implementation of <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4, T5}"/> in IoC and
        /// call <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4, T5}.Configure(TExtension, T1, T2, T3, T4, T5)"/>.
        /// If implementation not found in IoC then it do nothing. If found several implementations then it will call
        /// <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4, T5}.Configure(TExtension, T1, T2, T3, T4, T5)"/> at each.
        /// <seealso cref="IDroidNotificationExtension"/>,
        /// <seealso cref="ISnackbarExtension"/>,
        /// <seealso cref="IIosLocalNotificationExtension"/>,
        /// <seealso cref="IIosNotificationExtension"/>
        /// <seealso cref="IUwpExtension"/>
        /// </summary>
        /// <typeparam name="T1">A type to forward.</typeparam>
        /// <typeparam name="T2">A type to forward.</typeparam>
        /// <typeparam name="T3">A type to forward.</typeparam>
        /// <typeparam name="T4">A type to forward.</typeparam>
        /// <typeparam name="T5">A type to forward.</typeparam>
        /// <param name="a1">A data to forward.</param>
        /// <param name="a2">A data to forward.</param>
        /// <param name="a3">A data to forward.</param>
        /// <param name="a4">A data to forward.</param>
        /// <param name="a5">A data to forward.</param>
        /// <returns>Instance of current builder</returns>
        T Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5);
        /// <summary>
        /// This function will search an implementation of <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4, T5, T6}"/> in IoC and
        /// call <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4, T5, T6}.Configure(TExtension, T1, T2, T3, T4, T5, T6)"/>.
        /// If implementation not found in IoC then it do nothing. If found several implementations then it will call
        /// <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4, T5, T6}.Configure(TExtension, T1, T2, T3, T4, T5, T6)"/> at each.
        /// <seealso cref="IDroidNotificationExtension"/>,
        /// <seealso cref="ISnackbarExtension"/>,
        /// <seealso cref="IIosLocalNotificationExtension"/>,
        /// <seealso cref="IIosNotificationExtension"/>
        /// <seealso cref="IUwpExtension"/>
        /// </summary>
        /// <typeparam name="T1">A type to forward.</typeparam>
        /// <typeparam name="T2">A type to forward.</typeparam>
        /// <typeparam name="T3">A type to forward.</typeparam>
        /// <typeparam name="T4">A type to forward.</typeparam>
        /// <typeparam name="T5">A type to forward.</typeparam>
        /// <typeparam name="T6">A type to forward.</typeparam>
        /// <param name="a1">A data to forward.</param>
        /// <param name="a2">A data to forward.</param>
        /// <param name="a3">A data to forward.</param>
        /// <param name="a4">A data to forward.</param>
        /// <param name="a5">A data to forward.</param>
        /// <param name="a6">A data to forward.</param>
        /// <returns>Instance of current builder</returns>
        T Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6);
        /// <summary>
        /// This function will search an implementation of <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4, T5, T6, T7}"/> in IoC and
        /// call <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4, T5, T6, T7}.Configure(TExtension, T1, T2, T3, T4, T5, T6, T7)"/>.
        /// If implementation not found in IoC then it do nothing. If found several implementations then it will call
        /// <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4, T5, T6, T7}.Configure(TExtension, T1, T2, T3, T4, T5, T6, T7)"/> at each.
        /// <seealso cref="IDroidNotificationExtension"/>,
        /// <seealso cref="ISnackbarExtension"/>,
        /// <seealso cref="IIosLocalNotificationExtension"/>,
        /// <seealso cref="IIosNotificationExtension"/>
        /// <seealso cref="IUwpExtension"/>
        /// </summary>
        /// <typeparam name="T1">A type to forward.</typeparam>
        /// <typeparam name="T2">A type to forward.</typeparam>
        /// <typeparam name="T3">A type to forward.</typeparam>
        /// <typeparam name="T4">A type to forward.</typeparam>
        /// <typeparam name="T5">A type to forward.</typeparam>
        /// <typeparam name="T6">A type to forward.</typeparam>
        /// <typeparam name="T7">A type to forward.</typeparam>
        /// <param name="a1">A data to forward.</param>
        /// <param name="a2">A data to forward.</param>
        /// <param name="a3">A data to forward.</param>
        /// <param name="a4">A data to forward.</param>
        /// <param name="a5">A data to forward.</param>
        /// <param name="a6">A data to forward.</param>
        /// <param name="a7">A data to forward.</param>
        /// <returns>Instance of current builder</returns>
        T Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7);
        /// <summary>
        /// This function will search an implementation of <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4, T5, T6, T7, T8}"/> in IoC and
        /// call <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4, T5, T6, T7, T8}.Configure(TExtension, T1, T2, T3, T4, T5, T6, T7, T8)"/>.
        /// If implementation not found in IoC then it do nothing. If found several implementations then it will call
        /// <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4, T5, T6, T7, T8}.Configure(TExtension, T1, T2, T3, T4, T5, T6, T7, T8)"/>
        /// at each.
        /// <seealso cref="IDroidNotificationExtension"/>,
        /// <seealso cref="ISnackbarExtension"/>,
        /// <seealso cref="IIosLocalNotificationExtension"/>,
        /// <seealso cref="IIosNotificationExtension"/>
        /// <seealso cref="IUwpExtension"/>
        /// </summary>
        /// <typeparam name="T1">A type to forward.</typeparam>
        /// <typeparam name="T2">A type to forward.</typeparam>
        /// <typeparam name="T3">A type to forward.</typeparam>
        /// <typeparam name="T4">A type to forward.</typeparam>
        /// <typeparam name="T5">A type to forward.</typeparam>
        /// <typeparam name="T6">A type to forward.</typeparam>
        /// <typeparam name="T7">A type to forward.</typeparam>
        /// <typeparam name="T8">A type to forward.</typeparam>
        /// <param name="a1">A data to forward.</param>
        /// <param name="a2">A data to forward.</param>
        /// <param name="a3">A data to forward.</param>
        /// <param name="a4">A data to forward.</param>
        /// <param name="a5">A data to forward.</param>
        /// <param name="a6">A data to forward.</param>
        /// <param name="a7">A data to forward.</param>
        /// <param name="a8">A data to forward.</param>
        /// <returns>Instance of current builder</returns>
        T Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8);
        /// <summary>
        /// This function will search an implementation of <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> in IoC and 
        /// call <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4, T5, T6, T7, T8, T9}.Configure(TExtension, T1, T2, T3, T4, T5, T6, T7, T8, T9)"/>.
        /// If implementation not found in IoC, then it do nothing. If found several implementations then it will call
        /// <see cref="IExtensionPlugin{TExtension, T1, T2, T3, T4, T5, T6, T7, T8, T9}.Configure(TExtension, T1, T2, T3, T4, T5, T6, T7, T8, T9)"/>
        /// at each.
        /// <seealso cref="IDroidNotificationExtension"/>,
        /// <seealso cref="ISnackbarExtension"/>,
        /// <seealso cref="IIosLocalNotificationExtension"/>,
        /// <seealso cref="IIosNotificationExtension"/>
        /// <seealso cref="IUwpExtension"/>
        /// </summary>
        /// <typeparam name="T1">A type to forward.</typeparam>
        /// <typeparam name="T2">A type to forward.</typeparam>
        /// <typeparam name="T3">A type to forward.</typeparam>
        /// <typeparam name="T4">A type to forward.</typeparam>
        /// <typeparam name="T5">A type to forward.</typeparam>
        /// <typeparam name="T6">A type to forward.</typeparam>
        /// <typeparam name="T7">A type to forward.</typeparam>
        /// <typeparam name="T8">A type to forward.</typeparam>
        /// <typeparam name="T9">A type to forward.</typeparam>
        /// <param name="a1">A data to forward.</param>
        /// <param name="a2">A data to forward.</param>
        /// <param name="a3">A data to forward.</param>
        /// <param name="a4">A data to forward.</param>
        /// <param name="a5">A data to forward.</param>
        /// <param name="a6">A data to forward.</param>
        /// <param name="a7">A data to forward.</param>
        /// <param name="a8">A data to forward.</param>
        /// <param name="a9">A data to forward.</param>
        /// <returns>Instance of current builder</returns>
        T Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9);
    }
}

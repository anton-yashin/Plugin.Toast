using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast.Abstractions
{
    /// <summary>
    /// Interface to create a plugin to forward data to platform specific notification builders. You need to create
    /// an implementation of this interface, register it in your IoC, and then call
    /// <see cref="IBuilder.Add{T1}(T1)"/>
    /// or <see cref="IBuilderExtension{T}.Add{T1}(T1, T2)"/> to 
    /// call <see cref="Configure(TExtension, T1)"/>
    /// </summary>
    /// <typeparam name="TExtension">An extension type which you want to configure</typeparam>
    /// <typeparam name="T1">A type to forward</typeparam>
    /// <typeparam name="T2">A type to forward</typeparam>
    public interface IExtensionPlugin<TExtension, in T1>
        where TExtension : IBuilderExtension<TExtension>
    {
        /// <summary>
        /// This function will receive a forwarded data from
        /// <see cref="IBuilder.Add{T1}(T1)"/> or 
        /// <see cref="IBuilderExtension{T}.Add{T1}(T1)"/>
        /// </summary>
        /// <param name="extension">An extension object which need to be configured</param>
        /// <param name="a1">A forwarded data</param>
        void Configure(TExtension extension, T1 a1);
    }

    /// <summary>
    /// Interface to create a plugin to forward data to platform specific notification builders. You need to create
    /// an implementation of this interface, register it in your IoC, and then call
    /// <see cref="IBuilder.Add{T1, T2}(T1, T2)"/>
    /// or <see cref="IBuilderExtension{T}.Add{T1, T2}(T1, T2)"/> to 
    /// call <see cref="Configure(TExtension, T1, T2)"/>
    /// </summary>
    /// <typeparam name="TExtension">An extension type which you want to configure</typeparam>
    /// <typeparam name="T1">A type to forward</typeparam>
    /// <typeparam name="T2">A type to forward</typeparam>
    public interface IExtensionPlugin<TExtension, in T1, in T2>
        where TExtension : IBuilderExtension<TExtension>
    {
        /// <summary>
        /// This function will receive a forwarded data from
        /// <see cref="IBuilder.Add{T1, T2}(T1, T2)"/> or 
        /// <see cref="IBuilderExtension{T}.Add{T1, T2}(T1, T2)"/>
        /// </summary>
        /// <param name="extension">An extension object which need to be configured</param>
        /// <param name="a1">A forwarded data</param>
        /// <param name="a2">A forwarded data</param>
        void Configure(TExtension extension, T1 a1, T2 a2);
    }

    /// <summary>
    /// Interface to create a plugin to forward data to platform specific notification builders. You need to create
    /// an implementation of this interface, register it in your IoC, and then call
    /// <see cref="IBuilder.Add{T1, T2, T3}(T1, T2, T3)"/>
    /// or <see cref="IBuilderExtension{T}.Add{T1, T2, T3}(T1, T2, T3)"/> to 
    /// call <see cref="Configure(TExtension, T1, T2, T3)"/>
    /// </summary>
    /// <typeparam name="TExtension">An extension type which you want to configure</typeparam>
    /// <typeparam name="T1">A type to forward</typeparam>
    /// <typeparam name="T2">A type to forward</typeparam>
    /// <typeparam name="T3">A type to forward</typeparam>
    public interface IExtensionPlugin<TExtension, in T1, in T2, in T3>
        where TExtension : IBuilderExtension<TExtension>
    {
        /// <summary>
        /// This function will receive a forwarded data from
        /// <see cref="IBuilder.Add{T1, T2, T3}(T1, T2, T3)"/> or 
        /// <see cref="IBuilderExtension{T}.Add{T1, T2, T3}(T1, T2, T3)"/>
        /// </summary>
        /// <param name="extension">An extension object which need to be configured</param>
        /// <param name="a1">A forwarded data</param>
        /// <param name="a2">A forwarded data</param>
        /// <param name="a3">A forwarded data</param>
        void Configure(TExtension extension, T1 a1, T2 a2, T3 a3);
    }

    /// <summary>
    /// Interface to create a plugin to forward data to platform specific notification builders. You need to create
    /// an implementation of this interface, register it in your IoC, and then call
    /// <see cref="IBuilder.Add{T1, T2, T3, T4}(T1, T2, T3, T4)"/>
    /// or <see cref="IBuilderExtension{T}.Add{T1, T2, T3, T4}(T1, T2, T3, T4)"/> to 
    /// call <see cref="Configure(TExtension, T1, T2, T3, T4)"/>
    /// </summary>
    /// <typeparam name="TExtension">An extension type which you want to configure</typeparam>
    /// <typeparam name="T1">A type to forward</typeparam>
    /// <typeparam name="T2">A type to forward</typeparam>
    /// <typeparam name="T3">A type to forward</typeparam>
    /// <typeparam name="T4">A type to forward</typeparam>
    public interface IExtensionPlugin<TExtension, in T1, in T2, in T3, in T4>
        where TExtension : IBuilderExtension<TExtension>
    {
        /// <summary>
        /// This function will receive a forwarded data from
        /// <see cref="IBuilder.Add{T1, T2, T3, T4}(T1, T2, T3, T4)"/> or 
        /// <see cref="IBuilderExtension{T}.Add{T1, T2, T3, T4}(T1, T2, T3, T4)"/>
        /// </summary>
        /// <param name="extension">An extension object which need to be configured</param>
        /// <param name="a1">A forwarded data</param>
        /// <param name="a2">A forwarded data</param>
        /// <param name="a3">A forwarded data</param>
        /// <param name="a4">A forwarded data</param>
        void Configure(TExtension extension, T1 a1, T2 a2, T3 a3, T4 a4);
    }

    /// <summary>
    /// Interface to create a plugin to forward data to platform specific notification builders. You need to create
    /// an implementation of this interface, register it in your IoC, and then call
    /// <see cref="IBuilder.Add{T1, T2, T3, T4, T5}(T1, T2, T3, T4, T5)"/>
    /// or <see cref="IBuilderExtension{T}.Add{T1, T2, T3, T4, T5}(T1, T2, T3, T4, T5)"/> to 
    /// call <see cref="Configure(TExtension, T1, T2, T3, T4, T5)"/>
    /// </summary>
    /// <typeparam name="TExtension">An extension type which you want to configure</typeparam>
    /// <typeparam name="T1">A type to forward</typeparam>
    /// <typeparam name="T2">A type to forward</typeparam>
    /// <typeparam name="T3">A type to forward</typeparam>
    /// <typeparam name="T4">A type to forward</typeparam>
    /// <typeparam name="T5">A type to forward</typeparam>
    public interface IExtensionPlugin<TExtension, in T1, in T2, in T3, in T4, in T5>
        where TExtension : IBuilderExtension<TExtension>
    {
        /// <summary>
        /// This function will receive a forwarded data from
        /// <see cref="IBuilder.Add{T1, T2, T3, T4, T5}(T1, T2, T3, T4, T5)"/> or 
        /// <see cref="IBuilderExtension{T}.Add{T1, T2, T3, T4, T5}(T1, T2, T3, T4, T5)"/>
        /// </summary>
        /// <param name="extension">An extension object which need to be configured</param>
        /// <param name="a1">A forwarded data</param>
        /// <param name="a2">A forwarded data</param>
        /// <param name="a3">A forwarded data</param>
        /// <param name="a4">A forwarded data</param>
        /// <param name="a5">A forwarded data</param>
        void Configure(TExtension extension, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5);
    }

    /// <summary>
    /// Interface to create a plugin to forward data to platform specific notification builders. You need to create
    /// an implementation of this interface, register it in your IoC, and then call
    /// <see cref="IBuilder.Add{T1, T2, T3, T4, T5, T6}(T1, T2, T3, T4, T5, T6)"/>
    /// or <see cref="IBuilderExtension{T}.Add{T1, T2, T3, T4, T5, T6}(T1, T2, T3, T4, T5, T6)"/> to 
    /// call <see cref="Configure(TExtension, T1, T2, T3, T4, T5, T6)"/>
    /// </summary>
    /// <typeparam name="TExtension">An extension type which you want to configure</typeparam>
    /// <typeparam name="T1">A type to forward</typeparam>
    /// <typeparam name="T2">A type to forward</typeparam>
    /// <typeparam name="T3">A type to forward</typeparam>
    /// <typeparam name="T4">A type to forward</typeparam>
    /// <typeparam name="T5">A type to forward</typeparam>
    /// <typeparam name="T6">A type to forward</typeparam>
    public interface IExtensionPlugin<TExtension, in T1, in T2, in T3, in T4, in T5, in T6>
        where TExtension : IBuilderExtension<TExtension>
    {
        /// <summary>
        /// This function will receive a forwarded data from
        /// <see cref="IBuilder.Add{T1, T2, T3, T4, T5, T6}(T1, T2, T3, T4, T5, T6)"/> or 
        /// <see cref="IBuilderExtension{T}.Add{T1, T2, T3, T4, T5, T6}(T1, T2, T3, T4, T5, T6)"/>
        /// </summary>
        /// <param name="extension">An extension object which need to be configured</param>
        /// <param name="a1">A forwarded data</param>
        /// <param name="a2">A forwarded data</param>
        /// <param name="a3">A forwarded data</param>
        /// <param name="a4">A forwarded data</param>
        /// <param name="a5">A forwarded data</param>
        /// <param name="a6">A forwarded data</param>
        void Configure(TExtension extension, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6);
    }

    /// <summary>
    /// Interface to create a plugin to forward data to platform specific notification builders. You need to create
    /// an implementation of this interface, register it in your IoC, and then call
    /// <see cref="IBuilder.Add{T1, T2, T3, T4, T5, T6, T7}(T1, T2, T3, T4, T5, T6, T7)"/>
    /// or <see cref="IBuilderExtension{T}.Add{T1, T2, T3, T4, T5, T6, T7}(T1, T2, T3, T4, T5, T6, T7)"/> to 
    /// call <see cref="Configure(TExtension, T1, T2, T3, T4, T5, T6, T7)"/>
    /// </summary>
    /// <typeparam name="TExtension">An extension type which you want to configure</typeparam>
    /// <typeparam name="T1">A type to forward</typeparam>
    /// <typeparam name="T2">A type to forward</typeparam>
    /// <typeparam name="T3">A type to forward</typeparam>
    /// <typeparam name="T4">A type to forward</typeparam>
    /// <typeparam name="T5">A type to forward</typeparam>
    /// <typeparam name="T6">A type to forward</typeparam>
    /// <typeparam name="T7">A type to forward</typeparam>
    public interface IExtensionPlugin<TExtension, in T1, in T2, in T3, in T4, in T5, in T6, in T7>
        where TExtension : IBuilderExtension<TExtension>
    {
        /// <summary>
        /// This function will receive a forwarded data from
        /// <see cref="IBuilder.Add{T1, T2, T3, T4, T5, T6, T7}(T1, T2, T3, T4, T5, T6, T7)"/> or 
        /// <see cref="IBuilderExtension{T}.Add{T1, T2, T3, T4, T5, T6, T7}(T1, T2, T3, T4, T5, T6, T7)"/>
        /// </summary>
        /// <param name="extension">An extension object which need to be configured</param>
        /// <param name="a1">A forwarded data</param>
        /// <param name="a2">A forwarded data</param>
        /// <param name="a3">A forwarded data</param>
        /// <param name="a4">A forwarded data</param>
        /// <param name="a5">A forwarded data</param>
        /// <param name="a6">A forwarded data</param>
        /// <param name="a7">A forwarded data</param>
        void Configure(TExtension extension, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7);
    }

    /// <summary>
    /// Interface to create a plugin to forward data to platform specific notification builders. You need to create
    /// an implementation of this interface, register it in your IoC, and then call
    /// <see cref="IBuilder.Add{T1, T2, T3, T4, T5, T6, T7, T8}(T1, T2, T3, T4, T5, T6, T7, T8)"/>
    /// or <see cref="IBuilderExtension{T}.Add{T1, T2, T3, T4, T5, T6, T7, T8}(T1, T2, T3, T4, T5, T6, T7, T8)"/> to 
    /// call <see cref="Configure(TExtension, T1, T2, T3, T4, T5, T6, T7, T8)"/>
    /// </summary>
    /// <typeparam name="TExtension">An extension type which you want to configure</typeparam>
    /// <typeparam name="T1">A type to forward</typeparam>
    /// <typeparam name="T2">A type to forward</typeparam>
    /// <typeparam name="T3">A type to forward</typeparam>
    /// <typeparam name="T4">A type to forward</typeparam>
    /// <typeparam name="T5">A type to forward</typeparam>
    /// <typeparam name="T6">A type to forward</typeparam>
    /// <typeparam name="T7">A type to forward</typeparam>
    /// <typeparam name="T8">A type to forward</typeparam>
    public interface IExtensionPlugin<TExtension, in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8>
        where TExtension : IBuilderExtension<TExtension>
    {
        /// <summary>
        /// This function will receive a forwarded data from
        /// <see cref="IBuilder.Add{T1, T2, T3, T4, T5, T6, T7, T8}(T1, T2, T3, T4, T5, T6, T7, T8)"/> or 
        /// <see cref="IBuilderExtension{T}.Add{T1, T2, T3, T4, T5, T6, T7, T8}(T1, T2, T3, T4, T5, T6, T7, T8)"/>
        /// </summary>
        /// <param name="extension">An extension object which need to be configured</param>
        /// <param name="a1">A forwarded data</param>
        /// <param name="a2">A forwarded data</param>
        /// <param name="a3">A forwarded data</param>
        /// <param name="a4">A forwarded data</param>
        /// <param name="a5">A forwarded data</param>
        /// <param name="a6">A forwarded data</param>
        /// <param name="a7">A forwarded data</param>
        /// <param name="a8">A forwarded data</param>
        void Configure(TExtension extension, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8);
    }

    /// <summary>
    /// Interface to create a plugin to forward data to platform specific notification builders. You need to create
    /// an implementation of this interface, register it in your IoC, and then call
    /// <see cref="IBuilder.Add{T1, T2, T3, T4, T5, T6, T7, T8, T9}(T1, T2, T3, T4, T5, T6, T7, T8, T9)"/>
    /// or <see cref="IBuilderExtension{T}.Add{T1, T2, T3, T4, T5, T6, T7, T8, T9}(T1, T2, T3, T4, T5, T6, T7, T8, T9)"/> to 
    /// call <see cref="Configure(TExtension, T1, T2, T3, T4, T5, T6, T7, T8, T9)"/>
    /// </summary>
    /// <typeparam name="TExtension">An extension type which you want to configure</typeparam>
    /// <typeparam name="T1">A type to forward</typeparam>
    /// <typeparam name="T2">A type to forward</typeparam>
    /// <typeparam name="T3">A type to forward</typeparam>
    /// <typeparam name="T4">A type to forward</typeparam>
    /// <typeparam name="T5">A type to forward</typeparam>
    /// <typeparam name="T6">A type to forward</typeparam>
    /// <typeparam name="T7">A type to forward</typeparam>
    /// <typeparam name="T8">A type to forward</typeparam>
    /// <typeparam name="T9">A type to forward</typeparam>
    public interface IExtensionPlugin<TExtension, in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9>
        where TExtension : IBuilderExtension<TExtension>
    {
        /// <summary>
        /// This function will receive a forwarded data from
        /// <see cref="IBuilder.Add{T1, T2, T3, T4, T5, T6, T7, T8, T9}(T1, T2, T3, T4, T5, T6, T7, T8, T9)"/> or 
        /// <see cref="IBuilderExtension{T}.Add{T1, T2, T3, T4, T5, T6, T7, T8, T9}(T1, T2, T3, T4, T5, T6, T7, T8, T9)"/>
        /// </summary>
        /// <param name="extension">An extension object which need to be configured</param>
        /// <param name="a1">A forwarded data</param>
        /// <param name="a2">A forwarded data</param>
        /// <param name="a3">A forwarded data</param>
        /// <param name="a4">A forwarded data</param>
        /// <param name="a5">A forwarded data</param>
        /// <param name="a6">A forwarded data</param>
        /// <param name="a7">A forwarded data</param>
        /// <param name="a8">A forwarded data</param>
        /// <param name="a9">A forwarded data</param>
        void Configure(TExtension extension, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface IExtensionPlugin<TExtension, in T1>
        where TExtension : IBuilderExtension<TExtension>
    {
        void Configure(TExtension extension, T1 a1);
    }

    public interface IExtensionPlugin<TExtension, in T1, in T2>
        where TExtension : IBuilderExtension<TExtension>
    {
        void Configure(TExtension extension, T1 a1, T2 a2);
    }

    public interface IExtensionPlugin<TExtension, in T1, in T2, in T3>
        where TExtension : IBuilderExtension<TExtension>
    {
        void Configure(TExtension extension, T1 a1, T2 a2, T3 a3);
    }

    public interface IExtensionPlugin<TExtension, in T1, in T2, in T3, in T4>
        where TExtension : IBuilderExtension<TExtension>
    {
        void Configure(TExtension extension, T1 a1, T2 a2, T3 a3, T4 a4);
    }

    public interface IExtensionPlugin<TExtension, in T1, in T2, in T3, in T4, in T5>
        where TExtension : IBuilderExtension<TExtension>
    {
        void Configure(TExtension extension, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5);
    }

    public interface IExtensionPlugin<TExtension, in T1, in T2, in T3, in T4, in T5, in T6>
        where TExtension : IBuilderExtension<TExtension>
    {
        void Configure(TExtension extension, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6);
    }

    public interface IExtensionPlugin<TExtension, in T1, in T2, in T3, in T4, in T5, in T6, in T7>
        where TExtension : IBuilderExtension<TExtension>
    {
        void Configure(TExtension extension, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7);
    }

    public interface IExtensionPlugin<TExtension, in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8>
        where TExtension : IBuilderExtension<TExtension>
    {
        void Configure(TExtension extension, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8);
    }

    public interface IExtensionPlugin<TExtension, in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9>
        where TExtension : IBuilderExtension<TExtension>
    {
        void Configure(TExtension extension, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9);
    }
}

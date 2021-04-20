using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast.Abstractions
{

    /// <summary>
    /// A configuration that applied after a <see cref="IBuilder"/> is created.
    /// Create an implementation and register it in your IoC or use with <see cref="IBuilderExtension{T}.Use(IExtensionConfiguration{T})"/>
    /// </summary>
    /// <typeparam name="T">Type of platform extension.</typeparam>
    public interface IExtensionConfiguration<T>
        where T : IBuilderExtension<T>
    {
        /// <summary>
        /// build a notification using <see cref="IBuilderExtension{T}"/>
        /// </summary>
        void Configure(T builderExtension);
    }
}

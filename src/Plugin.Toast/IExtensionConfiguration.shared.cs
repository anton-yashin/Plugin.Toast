using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface IExtensionConfiguration<T>
        where T : IBuilderExtension<T>
    {
        /// <summary>
        /// build a notification using <see cref="IBuilderExtension{T}"/>
        /// </summary>
        void Configure(T builderExtension);
    }
}

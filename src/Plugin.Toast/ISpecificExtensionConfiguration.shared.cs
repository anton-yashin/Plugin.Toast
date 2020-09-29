using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// A configuration that may be applied after <see cref="IBuilder.UseConfiguration{T}(T)"/> if parameter
    /// equals to <see cref="Token"/>
    /// </summary>
    /// <typeparam name="TExtension">Extension type to use with</typeparam>
    /// <typeparam name="TToken">Token type</typeparam>
    public interface ISpecificExtensionConfiguration<TExtension, TToken> : IExtensionConfiguration<TExtension>
        where TExtension : IBuilderExtension<TExtension>
    {
        /// <summary>
        /// A value to match with <see cref="IBuilder.UseConfiguration{T}(T)"/> parameter.
        /// </summary>
        TToken Token { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// For use with dependency injection and <see cref="IBuilder.UseConfiguration{T}(T)"/>
    /// </summary>
    /// <typeparam name="TExtension">Extension type to use with</typeparam>
    /// <typeparam name="TToken">Token type</typeparam>
    public interface ISpecificExtensionConfiguration<TExtension, TToken> : IExtensionConfiguration<TExtension>
        where TExtension : IBuilderExtension<TExtension>
    {
        TToken Token { get; }
    }
}

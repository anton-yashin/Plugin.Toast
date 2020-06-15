using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Plugin.Toast
{
    static class BuilderTools
    {
        public static void UseConfigurationFrom<TSelf, TToken>(this TSelf builder, IServiceProvider? serviceProvider, TToken token)
            where TSelf: IBuilderExtension<TSelf>
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            if (serviceProvider != null)
            {
                var ec = EqualityComparer<TToken>.Default;
                foreach (var i in serviceProvider.GetService<IEnumerable<ISpecificExtensionConfiguration<TSelf, TToken>>>())
                {
                    if (ec.Equals(i.Token, token))
                    {
                        i.Configure(builder);
                    }
                }
            }
        }

        public static void UseConfigurationFrom<TSelf>(this TSelf builder, IServiceProvider? serviceProvider)
            where TSelf : IBuilderExtension<TSelf>
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            if (serviceProvider != null)
            {
                foreach (var i in serviceProvider.GetService<IEnumerable<IExtensionConfiguration<TSelf>>>())
                {
                    i.Configure(builder);
                }
            }
        }
    }
}

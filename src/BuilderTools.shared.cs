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

        public static void UsePlugin<TSelf, T1>(this TSelf builder, IServiceProvider? serviceProvider, T1 a1)
            where TSelf : IBuilderExtension<TSelf>
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            if (serviceProvider != null)
            {
                foreach (var i in serviceProvider.GetService<IEnumerable<IExtensionPlugin<TSelf, T1>>>())
                {
                    i.Configure(builder, a1);
                }
            }
        }

        public static void UsePlugin<TSelf, T1, T2>(this TSelf builder, IServiceProvider? serviceProvider,
            T1 a1, T2 a2)
            where TSelf : IBuilderExtension<TSelf>
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            if (serviceProvider != null)
            {
                foreach (var i in serviceProvider.GetService<IEnumerable<IExtensionPlugin<TSelf, T1, T2>>>())
                {
                    i.Configure(builder, a1, a2);
                }
            }
        }

        public static void UsePlugin<TSelf, T1, T2, T3>(this TSelf builder, IServiceProvider? serviceProvider,
            T1 a1, T2 a2, T3 a3)
            where TSelf : IBuilderExtension<TSelf>
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            if (serviceProvider != null)
            {
                foreach (var i in serviceProvider.GetService<IEnumerable<IExtensionPlugin<TSelf, T1, T2, T3>>>())
                {
                    i.Configure(builder, a1, a2, a3);
                }
            }
        }

        public static void UsePlugin<TSelf, T1, T2, T3, T4>(this TSelf builder, IServiceProvider? serviceProvider,
            T1 a1, T2 a2, T3 a3, T4 a4)
            where TSelf : IBuilderExtension<TSelf>
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            if (serviceProvider != null)
            {
                foreach (var i in serviceProvider.GetService<IEnumerable<IExtensionPlugin<TSelf, T1, T2, T3, T4>>>())
                {
                    i.Configure(builder, a1, a2, a3, a4);
                }
            }
        }

        public static void UsePlugin<TSelf, T1, T2, T3, T4, T5>(this TSelf builder, IServiceProvider? serviceProvider,
            T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
            where TSelf : IBuilderExtension<TSelf>
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            if (serviceProvider != null)
            {
                foreach (var i in serviceProvider.GetService<IEnumerable<IExtensionPlugin<TSelf, T1, T2, T3, T4, T5>>>())
                {
                    i.Configure(builder, a1, a2, a3, a4, a5);
                }
            }
        }

        public static void UsePlugin<TSelf, T1, T2, T3, T4, T5, T6>(this TSelf builder, IServiceProvider? serviceProvider,
            T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
            where TSelf : IBuilderExtension<TSelf>
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            if (serviceProvider != null)
            {
                foreach (var i in serviceProvider.GetService<IEnumerable<IExtensionPlugin<TSelf, T1, T2, T3, T4, T5, T6>>>())
                {
                    i.Configure(builder, a1, a2, a3, a4, a5, a6);
                }
            }
        }

        public static void UsePlugin<TSelf, T1, T2, T3, T4, T5, T6, T7>(this TSelf builder, IServiceProvider? serviceProvider,
            T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
            where TSelf : IBuilderExtension<TSelf>
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            if (serviceProvider != null)
            {
                foreach (var i in serviceProvider.GetService<IEnumerable<IExtensionPlugin<TSelf, T1, T2, T3, T4, T5, T6, T7>>>())
                {
                    i.Configure(builder, a1, a2, a3, a4, a5, a6, a7);
                }
            }
        }

        public static void UsePlugin<TSelf, T1, T2, T3, T4, T5, T6, T7, T8>(this TSelf builder, IServiceProvider? serviceProvider,
            T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
            where TSelf : IBuilderExtension<TSelf>
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            if (serviceProvider != null)
            {
                foreach (var i in serviceProvider.GetService<IEnumerable<IExtensionPlugin<TSelf, T1, T2, T3, T4, T5, T6, T7, T8>>>())
                {
                    i.Configure(builder, a1, a2, a3, a4, a5, a6, a7, a8);
                }
            }
        }

        public static void UsePlugin<TSelf, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this TSelf builder, IServiceProvider? serviceProvider,
            T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
            where TSelf : IBuilderExtension<TSelf>
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            if (serviceProvider != null)
            {
                foreach (var i in serviceProvider.GetService<IEnumerable<IExtensionPlugin<TSelf, T1, T2, T3, T4, T5, T6, T7, T8, T9>>>())
                {
                    i.Configure(builder, a1, a2, a3, a4, a5, a6, a7, a8, a9);
                }
            }
        }
    }
}

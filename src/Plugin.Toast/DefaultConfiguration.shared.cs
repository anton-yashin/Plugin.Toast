using System;

namespace Plugin.Toast
{
    sealed class DefaultConfiguration<T> : IExtensionConfiguration<T>
        where T : IBuilderExtension<T>
    {
        private readonly Action<T> action;

        public DefaultConfiguration(Action<T> action)
            => this.action = action;

        public void Configure(T builderExtension)
            => action(builderExtension);
    }
}

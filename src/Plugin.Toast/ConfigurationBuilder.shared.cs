using Microsoft.Extensions.DependencyInjection;
using System;

namespace Plugin.Toast
{
    sealed class ConfigurationBuilder : IConfigurationBuilder
    {
        public ConfigurationBuilder(IServiceCollection services)
            => Services = services;

        public IServiceCollection Services { get; }
    }
}

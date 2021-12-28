using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary/>
    public interface IConfigurationBuilder
    {
        /// <summary/>
        IServiceCollection Services { get; }
    }
}

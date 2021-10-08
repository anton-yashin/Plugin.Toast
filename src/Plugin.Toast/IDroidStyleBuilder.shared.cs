using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Interface to android style builder.
    /// </summary>
    public interface IDroidStyleBuilder 
    {
#if __ANDROID__
        /// <summary>
        /// Build platform specific notification style
        /// </summary>
        AndroidX.Core.App.NotificationCompat.Style Build();
#endif
    }
}

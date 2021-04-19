using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    /// <summary>
    /// Abstraction for platform initialization
    /// </summary>
    public interface IInitialization
    {
        /// <summary>
        /// Initialize current platform if need.
        /// </summary>
        /// <exception cref="Exceptions.NotificationException">On iOS if permission is deniend</exception>
        /// <remarks>
        /// Actual implementation available only on iOS. It request a
        /// permission to show notification. Other platforms do have a dummy
        /// implementation.
        /// </remarks>
        Task InitializeAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    public interface IInitialization
    {
        /// <summary>
        /// Initialize current platform if need.
        /// </summary>
        /// <exception cref="Exceptions.NotificationException"/>
        Task InitializeAsync();
    }
}

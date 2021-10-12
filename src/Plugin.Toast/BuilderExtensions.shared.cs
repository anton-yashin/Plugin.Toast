using Plugin.Toast.Abstractions;
using System;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    /// <summary>
    /// Extensions for <see cref="IBuilder"/>
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// Builds a notification.
        /// </summary>
        public static async Task<INotification> BuildAsync(this Task<IBuilder> @this)
        {
            var builder = await @this;
            return builder.Build();
        }
    }
}

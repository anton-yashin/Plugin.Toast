using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    public static class BuilderExtensions
    {
        public static async Task<INotification> Build(this Task<IBuilder> @this)
        {
            var builder = await @this;
            return builder.Build();
        }
    }
}

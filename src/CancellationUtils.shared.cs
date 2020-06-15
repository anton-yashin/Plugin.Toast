using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    static class CancellationUtils
    {
        public static async Task<T> WatchCancellationAsync<T>(
            this TaskCompletionSource<T> @this,
            CancellationToken cancellationToken,
            Action onCancellation)
        {
            using (cancellationToken.Register(() =>
            {
                @this.TrySetCanceled(cancellationToken);
                onCancellation();
            }))
            {
                return await @this.Task;
            }
        }
    }
}

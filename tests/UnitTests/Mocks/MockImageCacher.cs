using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
    public sealed class MockImageCacher : IImageCacher
    {
        public Func<string, CancellationToken, Func<Stream, CancellationToken, Task>, Task<string>>? OnCacheAsync;
        public int CacheAsyncCallCount { get; private set; }

        public Task<string> CacheAsync(string relativePath, CancellationToken cancellationToken, Func<Stream, CancellationToken, Task> copyToAsync)
        {
            CacheAsyncCallCount++;
            return OnCacheAsync?.Invoke(relativePath, cancellationToken, copyToAsync) ?? Task.FromResult("");
        }
    }
}

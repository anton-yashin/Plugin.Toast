using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
    public sealed class MockMimeDetector : IMimeDetector
    {
        public int DetectAsyncCallCount { get; private set; }
        public Func<Stream, CancellationToken, Task<string>>? OnDetectAsync;

        public Task<string> DetectAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            DetectAsyncCallCount++;
            if (OnDetectAsync != null)
                return OnDetectAsync(stream, cancellationToken);
            return Task.FromResult("application/octet-stream");
        }
    }
}

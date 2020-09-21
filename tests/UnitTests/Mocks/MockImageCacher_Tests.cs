using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Mocks
{
    public class MockImageCacher_Tests
    {
        [Fact]
        public void Construct()
        {
            // prepare
            var mic = new MockImageCacher();

            // verify
            Assert.Equal(expected: 0, mic.CacheAsyncCallCount);
        }

        [Fact]
        public async Task CacheAsyncCallCout()
        {
            // prepare
            var mic = new MockImageCacher();

            // act
            var result = await mic.CacheAsync("", default, (a, b) => Task.CompletedTask);

            // verify
            Assert.Equal("", result);
            Assert.Equal(expected: 1, mic.CacheAsyncCallCount);
        }

        [Fact]
        public async Task CacheAsyncCallback()
        {
            // prepare
            string expectedPath = Guid.NewGuid().ToString();
            string expectedResult = Guid.NewGuid().ToString();
            Func<Stream, CancellationToken, Task> expectedTask = (a, b) => Task.CompletedTask;
            var mic = new MockImageCacher();
            mic.OnCacheAsync = (p, ct, ca) =>
            {
                Assert.Same(expectedPath, p);
                Assert.Same(expectedTask, ca);
                return Task.FromResult(expectedResult);
            };

            // act
            var result = await mic.CacheAsync(expectedPath, default, expectedTask);

            // verify
            Assert.Same(expectedResult, result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Mocks
{
    public class MockMimeDetector_Tests
    {
        [Fact]
        public void Construct()
        {
            // prepare
            var mock = new MockMimeDetector();

            // verify
            Assert.Equal(0, mock.DetectAsyncCallCount);
        }

        [Fact]
        public async Task DetectAsyncCallCount()
        {
            // prepare
            var mock = new MockMimeDetector();

            // act
            var result = await mock.DetectAsync(new MemoryStream());

            // verify
            Assert.Equal(1, mock.DetectAsyncCallCount);
            Assert.Equal("application/octet-stream", result);
        }

        [Fact]
        public async Task DetectAsyncCallback()
        {
            // prepare
            var expectedResult = Guid.NewGuid().ToString();
            var expectedStream = new MemoryStream();
            var mock = new MockMimeDetector();
            mock.OnDetectAsync = (s, ct) =>
            {
                Assert.Same(expectedStream, s);
                return Task.FromResult(expectedResult);
            };

            // act
            var result = await mock.DetectAsync(expectedStream);

            // verify
            Assert.Equal(expectedResult, result);
        }
    }
}

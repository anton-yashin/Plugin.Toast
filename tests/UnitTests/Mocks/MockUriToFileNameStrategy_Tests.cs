using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests.Mocks
{
    public class MockUriToFileNameStrategy_Tests
    {
        [Fact]
        public void Construct()
        {
            // prepare
            var mock = new MockUriToFileNameStrategy();

            // verify
            Assert.Equal(0, mock.ConvertCallCount);
        }

        [Fact]
        public void ConvertCallCount()
        {
            // prepare
            var mock = new MockUriToFileNameStrategy();

            var result = mock.Convert(new Uri("https://example.com"));

            // verify
            Assert.Equal(1, mock.ConvertCallCount);
            Assert.Equal("", result);
        }

        [Fact]
        public void ConvertCallback()
        {
            // prepare
            var expectedUri = new Uri("https://example.com");
            var expectedResult = Guid.NewGuid().ToString();
            var mock = new MockUriToFileNameStrategy();
            mock.OnConvert = u =>
            {
                Assert.Equal(expectedUri, u);
                return expectedResult;
            };

            var result = mock.Convert(expectedUri);

            // verify
            Assert.Equal(expectedResult, result);
        }
    }
}

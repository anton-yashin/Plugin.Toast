using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace UnitTests.Mocks
{
    public class MockHttpClientFactory_Tests
    {
        [Fact]
        public void Construct()
        {
            // prepare
            var mf = new MockHttpClientFactory();

            // verify
            Assert.Equal(0, mf.CreateClientCallCount);
        }

        [Fact]
        public void CreateClientCallCount()
        {
            // prepare
            var mf = new MockHttpClientFactory();

            // act
            using var result = mf.CreateClient("name");

            // verify
            Assert.NotNull(result);
            Assert.Equal(1, mf.CreateClientCallCount);
        }

        [Fact]
        public void CreateClientCallback()
        {
            // prepare
            using var expectedResult = new HttpClient();
            var expectedName = Guid.NewGuid().ToString();
            var mf = new MockHttpClientFactory();
            mf.OnCreateHttpClient = name =>
            {
                Assert.Same(expectedName, name);
                return expectedResult;
            };

            // act
            using var result = mf.CreateClient(expectedName);

            // verify
            Assert.Same(expectedResult, result);
        }
    }
}

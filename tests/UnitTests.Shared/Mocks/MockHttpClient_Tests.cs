using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Mocks
{
    public class MockHttpClient_Tests
    {
        [Fact]
        public async Task Construct()
        {
            // prepare
            var expectedResult = new HttpResponseMessage();
            var client = new MockHttpClient(expectedResult);

            // act
            var result = await client.GetAsync("https://example.com");

            // verify
            Assert.Same(expectedResult, result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Mocks
{
    public class MockResourceToFileNameStrategy_Tests
    {
        [Fact]
        public void Construct()
        {
            // prepare
            var strategy = new MockResourceToFileNameStrategy();

            // verify
            Assert.Equal(0, strategy.ConvertCallCount);
        }

        [Fact]
        public void ConvertCallCount()
        {
            // prepare
            var strategy = new MockResourceToFileNameStrategy();

            // act
            var result = strategy.Convert("", Assembly.GetExecutingAssembly());

            // verify
            Assert.Equal("", result);
            Assert.Equal(1, strategy.ConvertCallCount);
        }

        [Fact]
        public void Convert()
        {
            // prepare
            string expectedResult = Guid.NewGuid().ToString();
            string expectedPath = Guid.NewGuid().ToString();
            var expectedAssembly = GetType().Assembly;
            var strategy = new MockResourceToFileNameStrategy();
            strategy.OnConvert = (p, a) =>
            {
                // verify
                Assert.Same(expectedPath, p);
                Assert.Same(expectedAssembly, a);
                return expectedResult;
            };

            // act
            var result = strategy.Convert(expectedPath, expectedAssembly);

            // verify
            Assert.Same(expectedResult, result);
        }
    }

}

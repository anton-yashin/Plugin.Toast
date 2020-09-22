using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests.Mocks
{
    public class MockBundleToFileNameStrategy_Tests
    {
        [Fact]
        public void Construct()
        {
            // prepare
            var mock = new MockBundleToFileNameStrategy();

            // verify
            Assert.Equal(0, mock.ConvertCallCount);
        }

        [Fact]
        public void ConvertCallCount()
        {
            // prepare
            var mock = new MockBundleToFileNameStrategy();

            // act
            var result = mock.Convert(Guid.NewGuid().ToString());

            // verify
            Assert.Equal(1, mock.ConvertCallCount);
            Assert.Equal("", result);
        }

        [Fact]
        public void ConvertCallback()
        {
            // prepare
            var expectedResult = Guid.NewGuid().ToString();
            var expectedArg = Guid.NewGuid().ToString();
            var mock = new MockBundleToFileNameStrategy();
            mock.OnConvert = s =>
            {
                Assert.Equal(expectedArg, s);
                return expectedResult;
            };

            // act
            var result = mock.Convert(expectedArg);

            // verify
            Assert.Equal(expectedResult, result);
        }
    }
}

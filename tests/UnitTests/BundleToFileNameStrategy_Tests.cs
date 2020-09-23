using Plugin.Toast;
using System;
using Xunit;

namespace UnitTests
{
    public class BundleToFileNameStrategy_Tests
    {
        [Fact]
        public void Convert()
        {
            var fileName = Guid.NewGuid().ToString();
            var strategy = new BundleToFileNameStrategy();

            var result = strategy.Convert(fileName);

            Assert.Equal(BundleToFileNameStrategy.KFolder + fileName, result);
        }
    }
}

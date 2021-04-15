using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests
{
    public class DefaultConfiguration_Tests
    {
        [Fact]
        public void Configure()
        {
            // prepare
            var mock = new MockBuilderExtension();
            var configuration = new DefaultConfiguration<MockBuilderExtension>(Verify);

            // act
            configuration.Configure(mock);

            // verify
            void Verify(MockBuilderExtension mbe) => Assert.Same(mock, mbe);
        }
    }
}

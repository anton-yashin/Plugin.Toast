﻿using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DeviceTests.Mocks
{
    public class MockSpecificExtensionConfiguration_Tests
    {
        [Fact]
        public void Construct()
        {
            // prepare
            var config = new MockSpecificExtensionConfiguration<MockExtension, int>();

            // verify
            Assert.Equal(expected: 0, config.ConfigureCallCount);
            Assert.Equal(expected: 0, config.TokenCallCount);
            Assert.Equal(expected: 0, config.Token);
        }

        [Fact]
        public void TokenCallCount()
        {
            // prepare
            var config = new MockSpecificExtensionConfiguration<MockExtension, int>();

            // act
            _ = config.Token;

            // verify
            Assert.Equal(expected: 1, config.TokenCallCount);
        }

        [Fact]
        public void TokenCallback()
        {
            // prepare
            const int KExpected = 1234;
            var config = new MockSpecificExtensionConfiguration<MockExtension, int>();
            config.OnToken = () => KExpected;

            // act
            var result = config.Token;

            // verify
            Assert.Equal(KExpected, result);
        }

        [Fact]
        public void ConfigureCallCount()
        {
            // prepare
            var me = new MockExtension();
            var config = new MockSpecificExtensionConfiguration<MockExtension, int>();

            // act
            config.Configure(me);

            // verify
            Assert.Equal(expected: 1, config.ConfigureCallCount);
        }

        [Fact]
        public void ConfigureCallback()
        {
            // prepare
            var me = new MockExtension();
            var config = new MockSpecificExtensionConfiguration<MockExtension, int>();

            // verify
            config.OnConfigure += e => Assert.Same(expected: me, e);

            // act
            config.Configure(me);
        }

        sealed class MockExtension : IBuilderExtension<MockExtension>
        {
            public MockExtension AddDescription(string description) => this;

            public MockExtension AddTitle(string title) => this;

            public MockExtension Use(IExtensionConfiguration<MockExtension> visitor) => this;
        }

    }
}
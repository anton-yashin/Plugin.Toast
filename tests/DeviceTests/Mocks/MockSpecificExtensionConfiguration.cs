using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DeviceTests.Mocks
{
    sealed class MockSpecificExtensionConfiguration<TExtension, TToken> : ISpecificExtensionConfiguration<TExtension, TToken>
        where TExtension : IBuilderExtension<TExtension>
    {
        public Func<TToken>? OnToken;
        public int TokenCallCount { get; set; }

        public TToken Token
        {
            get
            {
                TokenCallCount++;
                return OnToken != null ? OnToken() : default!;
            }
        }

        public Action<TExtension>? OnConfigure;
        public int ConfigureCallCount { get; set; }

        public void Configure(TExtension builderExtension)
        {
            ConfigureCallCount++;
            OnConfigure?.Invoke(builderExtension);
        }

        public void VerifyNoCalls()
        {
            Assert.Equal(0, TokenCallCount);
            Assert.Equal(0, ConfigureCallCount);
        }
    }
}

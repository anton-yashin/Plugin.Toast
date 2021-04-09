using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
    public sealed class MockHttpClient : HttpClient
    {
        public MockHttpClient(HttpResponseMessage result)
            : base(new MockHttpClientHandler(result))
        {
        }

        sealed class MockHttpClientHandler : DelegatingHandler
        {
            private readonly HttpResponseMessage result;

            public MockHttpClientHandler(HttpResponseMessage result)
            {
                this.result = result;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
                => Task.FromResult(result);
        }
    }
}

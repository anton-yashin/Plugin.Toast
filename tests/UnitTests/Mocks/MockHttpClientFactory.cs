using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace UnitTests.Mocks
{
    public sealed class MockHttpClientFactory : IHttpClientFactory
    {
        public int CreateClientCallCount { get; private set; }
        public Func<string, HttpClient>? OnCreateHttpClient;

        public HttpClient CreateClient(string name)
        {
            CreateClientCallCount++;
            if (OnCreateHttpClient != null)
                return OnCreateHttpClient(name);
            return new HttpClient();
        }
    }
}

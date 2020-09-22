using Plugin.Toast;
using System;

namespace UnitTests.Mocks
{
    public sealed class MockUriToFileNameStrategy : IUriToFileNameStrategy
    {
        public int ConvertCallCount { get; private set; }
        public Func<Uri, string>? OnConvert;

        public string Convert(Uri uri)
        {
            ConvertCallCount++;
            return OnConvert != null ? OnConvert(uri) : "";
        }
    }
}

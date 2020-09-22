using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace UnitTests.Mocks
{
    public sealed class MockResourceToFileNameStrategy : IResourceToFileNameStrategy
    {
        public int ConvertCallCount { get; private set; }
        public Func<string, Assembly, string>? OnConvert;

        public string Convert(string resourcePath, Assembly assembly)
        {
            ConvertCallCount++;
            return OnConvert?.Invoke(resourcePath, assembly) ?? "";
        }
    }
}

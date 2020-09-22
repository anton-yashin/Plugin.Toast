using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Mocks
{
    public sealed class MockBundleToFileNameStrategy : IBundleToFileNameStrategy
    {
        public int ConvertCallCount { get; private set; }
        public Func<string, string>? OnConvert;

        public string Convert(string bundlePath)
        {
            ConvertCallCount++;
            if (OnConvert != null)
                return OnConvert(bundlePath);
            return "";
        }
    }
}

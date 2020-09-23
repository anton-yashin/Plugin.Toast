using Plugin.Toast;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ImageCacher_Tests
    {
        [Theory, MemberData(nameof(GetCacheAsyncData))]
        public async Task CacheAsync(string subfolder)
        {
            // prepare
            var fileName = Guid.NewGuid().ToString()  + ".dat";
            var cacher = new ImageCacher();

            // act
            var result = await cacher.CacheAsync(subfolder + fileName, GetSomeData);

            // verify
            Assert.EndsWith(fileName, result);
            Assert.True(File.Exists(result));
            using (var file = File.OpenRead(result))
            using (var someData = GetSomeData())
                Assert.Equal(await GetContentAsync(someData), await GetContentAsync(file));
        }

        public static IEnumerable<object[]> GetCacheAsyncData()
        {
            yield return new object[] { "" };
            yield return new object[] { "subfolder/" };
        }

        static Stream GetSomeData() => Assembly.GetExecutingAssembly().GetManifestResourceStream("UnitTests.SomeData.txt")
                ?? throw new InvalidOperationException("test data not found");

        static async Task<byte[]> GetContentAsync(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var data = new byte[stream.Length];
            var read = await stream.ReadAsync(data, 0, data.Length);
            if (read != data.Length)
                throw new InvalidOperationException("cant read steam");
            return data;
        }
    }
}

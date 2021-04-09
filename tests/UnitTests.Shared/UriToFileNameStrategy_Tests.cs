using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class UriToFileNameStrategy_Tests
    {
        [Theory, MemberData(nameof(GetConvertTestData))]
        public void Convert(string expected, Uri arg)
        {
            // prepare
            var strategy = new UriToFileNameStrategy();

            // act
            var result = strategy.Convert(arg);

            //verify
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> GetConvertTestData()
        {
            var sb = new StringBuilder();
            var invalid = new string(Path.GetInvalidPathChars());
            foreach (var i in invalid)
                sb.Append('+').Append((int)i);
            yield return new object[] { UriToFileNameStrategy.KFolder + "https+++www.example.com/" + sb.ToString() + "some.png",
#pragma warning disable CS0618 // Type or member is obsolete: required unescaped invalid data inside uri
                new Uri("https://www.example.com/" + invalid + "some.png", true) };
#pragma warning restore CS0618 // Type or member is obsolete
            yield return new object[] { UriToFileNameStrategy.KFolder + "https+++www.example.com/some.png",
                new Uri("https://www.example.com/some.png", UriKind.Absolute) };
        }

    }
}

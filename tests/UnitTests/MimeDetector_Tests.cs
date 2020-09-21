﻿using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DeviceTests
{
    public class MimeDetector_Tests
    {
        [Theory, MemberData(nameof(GetTestData))]
        public async Task Test(byte[] data, string expected)
        {
            // prepare
            using var ms = new MemoryStream(data, writable: false);
            var md = new MimeDetector();

            // act
            var result = await md.DetectAsync(ms);

            // verify
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[] { new byte[] { (byte)'B', (byte)'M' }, MimeDetector.KBmpMime };
            yield return new object[] { new byte[] { (byte)'G', (byte)'I', (byte)'F' }, MimeDetector.KGifMime };
//            yield return new object[] { new byte[] { (byte)'F', (byte)'W', (byte)'S' }, MimeDetector.};
//            yield return new object[] { new byte[] { (byte)'C', (byte)'W', (byte)'S' }, MimeDetector.};
            yield return new object[] { new byte[] { 0xff, 0xd8, 0xff }, MimeDetector.KJpegMime};
            yield return new object[] { new byte[] { (byte)'8', (byte)'B', (byte)'P', (byte)'S'  }, MimeDetector.KPsdMime};
            yield return new object[] { new byte[] { (byte)'F', (byte)'O', (byte)'R', (byte)'M' }, MimeDetector.KIffMime};
            yield return new object[] { new byte[] { (byte)'R', (byte)'I', (byte)'F', (byte)'F' }, MimeDetector.KWebpMime};
            yield return new object[] { new byte[] { 0x00, 0x00, 0x01, 0x00 }, MimeDetector.KIcoMime };
            yield return new object[] { new byte[] { (byte)'I', (byte)'I', 0x2A, 0x00 }, MimeDetector.KTiffMime};
//            yield return new object[] { new byte[] { (byte)'M', (byte)'M', 0x00, 0x2A }, MimeDetector.};
            yield return new object[] { new byte[] { 0x89, 0x50, 0x4e, 0x47, 0x0d, 0x0a, 0x1a, 0x0a }, MimeDetector.KPngMime};
            yield return new object[] { new byte[] { 0x00, 0x00, 0x00, 0x0c, 0x6a, 0x50, 0x20, 0x20, 0x0d, 0x0a, 0x87, 0x0a }, MimeDetector.KJp2Mime};
        }
    }
}

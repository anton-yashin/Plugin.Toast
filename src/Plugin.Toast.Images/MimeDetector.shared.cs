using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    sealed class MimeDetector : IMimeDetector
    {
        internal const string KBmpMime = "image/x-ms-bmp";
        internal const string KGifMime = "image/gif";
        internal const string KJpegMime = "image/jpeg";
        internal const string KPsdMime = "image/psd";
        internal const string KIffMime = "image/iff";
        internal const string KWebpMime = "image/webp";
        internal const string KIcoMime = "image/vnd.microsoft.icon";
        internal const string KTiffMime = "image/tiff";
        internal const string KPngMime = "image/png";
        internal const string KJp2Mime = "image/jp2";

        const int KMaxRead = 12;

        public async Task<string> DetectAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            byte[] data = new byte[KMaxRead];
            var read = await stream.ReadAsync(data, 0, data.Length);
            if (read > 0)
            {
                // FIXME: generate from t4
                if (read >= 2 && data[0] == 'B' && data[1] == 'M')
                    return KBmpMime;
                if (read >= 3 && data[0] == 'G' && data[1] == 'I' && data[2] == 'F')
                    return KGifMime;
                if (read >= 3 && data[0] ==  0xff && data[1] == 0xd8 && data[2] == 0xff)
                    return KJpegMime;
                if (read >= 4 && data[0] == '8' && data[1] == 'B' && data[2] == 'P' && data[3] == 'S')
                    return KPsdMime;
                if (read >= 4 && data[0] == 'F' && data[1] == 'O' && data[2] == 'R' && data[3] == 'M')
                    return KIffMime;
                if (read >= 4 && data[0] == 'R' && data[1] == 'I' && data[2] == 'F' && data[3] == 'F')
                    return KWebpMime;
                if (read >= 4 && data[0] == 0x00 && data[1] == 0x00 && data[2] == 0x01 && data[3] == 0x00)
                    return KIcoMime;
                if (read >= 4 && data[0] == 'I' && data[1] == 'I' && data[2] == 0x2A && data[3] == 0x00)
                    return KTiffMime;
                if (read >= 8 && data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4e && data[3] == 0x47
                    && data[4] == 0x0d && data[5] == 0x0a && data[6] == 0x1a && data[7] == 0x0a)
                {
                    return KPngMime;
                }
                if (read >= 12 && data[0] == 0x00 && data[1] == 0x00 && data[2] == 0x00 && data[3] == 0x0c
                    && data[4] == 0x6a && data[5] == 0x50 && data[6] == 0x20 && data[7] == 0x20
                    && data[8] == 0x0d && data[9] == 0x0a && data[10] ==0x87 && data[11] == 0x0a)
                {
                    return KJp2Mime;
                }
            }
            return "application/octet-stream";
        }
    }

    public interface IMimeDetector
    {
        /// <summary>
        /// Detects mime by readable data stream.
        /// </summary>
        /// <param name="stream">Data stream</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task that contains a detected mime type. Default value is application/octet-stream</returns>
        /// <remarks>
        /// Default implementation read first 12 bytes and checks data
        /// to match a pattern one of following mime types: image/x-ms-bmp, image/gif, image/jpeg,
        /// image/psd, image/iff, image/webp, image/vnd.microsoft.icon, image/tiff, image/png, image/jp2
        /// </remarks>
        Task<string> DetectAsync(Stream stream, CancellationToken cancellationToken = default);
    }
}

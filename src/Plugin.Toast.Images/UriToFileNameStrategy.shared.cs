using System;
using System.IO;
using System.Linq;

namespace Plugin.Toast
{
    sealed class UriToFileNameStrategy : IUriToFileNameStrategy
    {
        public string Convert(Uri uri)
        {
            const string KFolder = "ToastImageSouce.FromUri/";
            var subfolder = uri.Scheme + "+++" + uri.Host + "/";
            var fn = uri.PathAndQuery;
            foreach (var i in Path.GetInvalidFileNameChars().Concat(Path.GetInvalidPathChars()))
                fn = fn.Replace(i.ToString(), "+" + (int)i);
            return Path.Combine(KFolder, subfolder, fn);
        }
    }

    public interface IUriToFileNameStrategy
    {
        string Convert(Uri uri);
    }
}

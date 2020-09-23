using System;
using System.IO;

namespace Plugin.Toast
{
    sealed class UriToFileNameStrategy : IUriToFileNameStrategy
    {
        internal const string KFolder = "ToastImageSouce.FromUri/";

        public string Convert(Uri uri)
        {
            var subfolder = uri.Scheme + "+++" + uri.Host + "/";
            var fn = uri.PathAndQuery;
            if (fn.Length > 0 && fn[0] == '/')
                fn = fn.Substring(1);
            foreach (var i in Path.GetInvalidFileNameChars())
                fn = fn.Replace(i.ToString(), "+" + (int)i);
            return Path.Combine(KFolder, subfolder, fn);
        }
    }

    public interface IUriToFileNameStrategy
    {
        string Convert(Uri uri);
    }
}

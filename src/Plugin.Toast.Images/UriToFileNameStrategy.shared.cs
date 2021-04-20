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

    /// <summary>
    /// Provides a conversion from uri to local file name.
    /// You can override default behaviour by registering in
    /// IoC a custom implementation.
    /// </summary>
    public interface IUriToFileNameStrategy
    {
        /// <summary>
        /// Converts uri to local file name.
        /// </summary>
        /// <param name="uri">The uri to covert.</param>
        /// <returns>The local file path.</returns>
        string Convert(Uri uri);
    }
}

using System;
using System.IO;
using System.Reflection;

namespace Plugin.Toast
{
    sealed class ResourceToFileNameStrategy : IResourceToFileNameStrategy
    {
        internal const string KFolder = "ToastImageSource.FromResource/";

        public string Convert(string resourcePath, Assembly assembly)
        {
            var asn = assembly.GetName();
            return Path.Combine(KFolder, asn.Name + "_" + asn.Version + "_" + resourcePath);
        }
    }

    /// <summary>
    /// Provides a conversion from resource name to local file name.
    /// You can override default behaviour by registering in 
    /// IoC a custom implementation.
    /// </summary>
    public interface IResourceToFileNameStrategy
    {
        string Convert(string resourcePath, Assembly assembly);
    }
}

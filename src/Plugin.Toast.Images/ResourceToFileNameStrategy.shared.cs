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

    public interface IResourceToFileNameStrategy
    {
        string Convert(string resourcePath, Assembly assembly);
    }
}

using System;
using System.IO;
using System.Reflection;

namespace Plugin.Toast
{
    sealed class ResourceToFileNameStrategy : IResourceToFileNameStrategy
    {
        public string Convert(string resourcePath, Assembly assembly)
        {
            var asn = assembly.GetName();
            return Path.Combine("ToastImageSource.FromResource/", asn.Name + "_" + asn.Version + "_" + resourcePath);
        }
    }

    internal interface IResourceToFileNameStrategy
    {
        string Convert(string resourcePath, Assembly assembly);
    }
}

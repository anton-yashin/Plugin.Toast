using System;

namespace Plugin.Toast.Images
{
    static class PackageInfo
    {
        private static readonly Lazy<bool> _isPackagedLazy = new Lazy<bool>(IsPackaged);
        internal static string GetRoot()
        {
            return _isPackagedLazy.Value
                ? Windows.ApplicationModel.Package.Current.InstalledLocation.Path
                : AppContext.BaseDirectory;
        }

        internal static bool IsPackaged()
        {
            try
            {
                if (Windows.ApplicationModel.Package.Current != null)
                    return true;
            }
            catch { }
            return false;
        }


    }
}

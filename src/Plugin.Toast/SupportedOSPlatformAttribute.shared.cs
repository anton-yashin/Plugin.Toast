using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast;
#if NET6_0_OR_GREATER == false

/// <summary>
/// Stub for previous versions of .NET
/// </summary>
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
internal class SupportedOSPlatformAttribute : Attribute
{
    public SupportedOSPlatformAttribute(string platformName) { }
}
#endif
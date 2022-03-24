using System;

namespace Plugin.Toast;
#if NET6_0_OR_GREATER == false

/// <summary>
/// Attribute stub for older versions .NET
/// </summary>
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
internal class UnsupportedOSPlatformAttribute : Attribute
{
    public UnsupportedOSPlatformAttribute(string platformName) { }
}

#endif

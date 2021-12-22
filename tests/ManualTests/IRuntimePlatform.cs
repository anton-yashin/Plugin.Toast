using System;
using System.Collections.Generic;
using System.Text;

namespace ManualTests
{
    public interface IRuntimePlatform
    {
        string RuntimePlatfrom { get; }

        bool IsIos { get; }
        bool IsAndroid { get; }
        bool IsWindows { get; }
        bool IsMaui { get; }
        bool IsXamarin { get; }
    }
}

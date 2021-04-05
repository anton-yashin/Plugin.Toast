#if NETCORE_APP == false
#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceTests.Utils
{
    class EnumUtils
    {
        public static IEnumerable<T> GetEnumValuesExclude<T>(params T[] exclude)
            => from i in Enum.GetValues(typeof(T)).Cast<T>()
               where exclude.Contains(i) == false
               select i;

    }
}

#endif
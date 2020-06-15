using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface IBuilderExtension<T>
        where T: IBuilderExtension<T>
    {
        T Use(IExtensionConfiguration<T> visitor);
        T AddTitle(string title);
        T AddDescription(string description);
    }
}

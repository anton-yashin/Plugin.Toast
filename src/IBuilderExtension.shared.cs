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
        T Add<T1>(T1 a1);
        T Add<T1, T2>(T1 a1, T2 a2);
        T Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3);
        T Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4);
        T Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5);
        T Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6);
        T Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7);
        T Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8);
        T Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9);

    }
}

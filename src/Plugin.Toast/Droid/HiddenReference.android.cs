using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Plugin.Toast.Droid
{
    sealed class HiddenReference<T>
    {
        static Dictionary<int, T> table = new Dictionary<int, T>();
        static int idgen = 0;

        readonly int id;

        public HiddenReference() => id = Interlocked.Increment(ref idgen);

        public HiddenReference(T t) : this() => Value = t;

        ~HiddenReference()
        {
            lock (table) { table.Remove(id); }
        }

        public T Value
        {
            get { lock (table) { return table[id]; } }
            set { lock (table) { table[id] = value; } }
        }
    }
}

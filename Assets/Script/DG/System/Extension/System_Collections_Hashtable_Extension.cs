using System.Collections;
using System.Collections.Generic;

namespace DG
{
    public static class System_Collections_Hashtable_Extension
    {
        public static Dictionary<K, V> ToDict<K, V>(this Hashtable self)
        {
            return HashtableUtil.ToDict<K, V>(self);
        }

        public static T Get<T>(this Hashtable self, object key)
        {
            return HashtableUtil.Get<T>(self, key);
        }
    }
}
using System.Collections;
using UnityEngine;

namespace DG
{
    public static partial class UnityEngine_Transform_Extension
    {
        public static Hashtable GetSerializeHashtable(this Transform self)
        {
            return TransformUtil.GetSerializeHashtable(self);
        }

        public static void LoadSerializeHashtable(this Transform self, Hashtable hashtable)
        {
            TransformUtil.LoadSerializeHashtable(self, hashtable);
        }
    }
}
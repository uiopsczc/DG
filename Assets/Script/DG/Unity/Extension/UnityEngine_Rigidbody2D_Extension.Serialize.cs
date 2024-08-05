using System.Collections;
using UnityEngine;

namespace DG
{
    public static partial class UnityEngine_Rigidbody2D_Extension
    {
        public static Hashtable GetSerializeHashtable(this Rigidbody2D self)
        {
            return Rigidbody2DUtil.GetSerializeHashtable(self);
        }

        public static void LoadSerializeHashtable(this Rigidbody2D self, Hashtable hashtable)
        {
            Rigidbody2DUtil.LoadSerializeHashtable(self, hashtable);
        }
    }
}
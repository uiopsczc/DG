using System.Collections;
using UnityEngine;

namespace DG
{
    public static partial class UnityEngine_BoxCollider_Extension
    {
        public static Hashtable GetSerializeHashtable(this BoxCollider self)
        {
            return BoxColliderUtil.GetSerializeHashtable(self);
        }

        public static void LoadSerializeHashtable(this BoxCollider self, Hashtable hashtable)
        {
            BoxColliderUtil.LoadSerializeHashtable(self, hashtable);
        }
    }
}
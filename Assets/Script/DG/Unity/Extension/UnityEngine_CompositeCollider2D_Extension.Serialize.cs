using System.Collections;
using UnityEngine;

namespace DG
{
    public static class UnityEngine_CompositeCollider2D_Extension_Serialize
    {
        public static Hashtable GetSerializeHashtable(this CompositeCollider2D self)
        {
            return CompositeCollider2DUtil.GetSerializeHashtable(self);
        }

        public static void LoadSerializeHashtable(this CompositeCollider2D self, Hashtable hashtable)
        {
            CompositeCollider2DUtil.LoadSerializeHashtable(self, hashtable);
        }
    }
}
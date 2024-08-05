using System.Collections;
using UnityEngine;

namespace DG
{
    public static partial class UnityEngine_Grid_Extension
    {
        public static Hashtable GetSerializeHashtable(this Grid self)
        {
            return GridUtil.GetSerializeHashtable(self);
        }

        public static void LoadSerializeHashtable(this Grid self, Hashtable hashtable)
        {
            GridUtil.LoadSerializeHashtable(self, hashtable);
        }
    }
}
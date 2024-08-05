using System.Collections;
using UnityEngine;

namespace DG
{
    public static partial class BoxColliderUtil
    {
        public static Hashtable GetSerializeHashtable(BoxCollider boxCollider)
        {
            Hashtable hashtable = new Hashtable
            {
                [StringConst.STRING_IS_TRIGGER] = boxCollider.isTrigger,
                [StringConst.STRING_center] = boxCollider.center.ToStringOrDefault(),
                [StringConst.STRING_SIZE] = boxCollider.size.ToStringOrDefault(null, Vector3.one)
            };
            hashtable.Trim();
            return hashtable;
        }

        public static void LoadSerializeHashtable(BoxCollider boxCollider, Hashtable hashtable)
        {
            boxCollider.isTrigger = hashtable.Get<bool>(StringConst.STRING_IS_TRIGGER);
            boxCollider.center = hashtable.Get<string>(StringConst.STRING_center).ToVector3OrDefault();
            boxCollider.size = hashtable.Get<string>(StringConst.STRING_SIZE).ToVector3OrDefault(null, Vector3.one);
        }
    }
}
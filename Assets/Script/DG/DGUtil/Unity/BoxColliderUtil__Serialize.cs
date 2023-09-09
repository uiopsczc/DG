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
				[StringConst.String_isTrigger] = boxCollider.isTrigger,
				[StringConst.String_center] = boxCollider.center.ToStringOrDefault(),
				[StringConst.String_size] = boxCollider.size.ToStringOrDefault(null, Vector3.one)
			};
			hashtable.Trim();
			return hashtable;
		}

		public static void LoadSerializeHashtable(BoxCollider boxCollider, Hashtable hashtable)
		{
			boxCollider.isTrigger = hashtable.Get<bool>(StringConst.String_isTrigger);
			boxCollider.center = hashtable.Get<string>(StringConst.String_center).ToVector3OrDefault();
			boxCollider.size = hashtable.Get<string>(StringConst.String_size).ToVector3OrDefault(null, Vector3.one);
		}
	}
}
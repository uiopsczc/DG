using System.Collections;
using UnityEngine.Tilemaps;

namespace DG
{
	public static partial class UnityEngine_TilemapCollider2D_Extension
	{
		public static Hashtable GetSerializeHashtable(this TilemapCollider2D self)
		{
			return TilemapCollider2DUtil.GetSerializeHashtable(self);
		}

		public static void LoadSerializeHashtable(this TilemapCollider2D self, Hashtable hashtable)
		{
			TilemapCollider2DUtil.LoadSerializeHashtable(self, hashtable);
		}
	}
}


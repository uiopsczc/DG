using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DG
{
	public static partial class UnityEngine_TilemapRenderer_Extension
	{
		public static Hashtable GetSerializeHashtable(this TilemapRenderer self)
		{
			return TilemapRendererUtil.GetSerializeHashtable(self);
		}

		public static void LoadSerializeHashtable(this TilemapRenderer self, Hashtable hashtable)
		{
			TilemapRendererUtil.LoadSerializeHashtable(self, hashtable);
		}
	}
}


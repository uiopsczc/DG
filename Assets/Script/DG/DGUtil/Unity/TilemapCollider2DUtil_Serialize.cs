using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DG
{
	public partial class TilemapCollider2DUtil
	{
		public static Hashtable GetSerializeHashtable(TilemapCollider2D tilemapCollider2D)
		{
			Hashtable hashtable = new Hashtable
			{
				[StringConst.String_maximumTileChangeCount] = tilemapCollider2D.maximumTileChangeCount,
				[StringConst.String_extrusionFactor] = tilemapCollider2D.extrusionFactor,
				[StringConst.String_isTrigger] = tilemapCollider2D.isTrigger,
				[StringConst.String_usedByEffector] = tilemapCollider2D.usedByEffector,
				[StringConst.String_usedByComposite] = tilemapCollider2D.usedByComposite,
				[StringConst.String_offset] = tilemapCollider2D.offset.ToStringOrDefault()
			};
			hashtable.Trim();
			return hashtable;
		}

		public static void LoadSerializeHashtable(TilemapCollider2D tilemapCollider2D, Hashtable hashtable)
		{
			tilemapCollider2D.maximumTileChangeCount = hashtable.Get<uint>(StringConst.String_maximumTileChangeCount);
			tilemapCollider2D.extrusionFactor = hashtable.Get<float>(StringConst.String_extrusionFactor);
			tilemapCollider2D.isTrigger = hashtable.Get<bool>(StringConst.String_isTrigger);
			tilemapCollider2D.usedByEffector = hashtable.Get<bool>(StringConst.String_usedByEffector);
			tilemapCollider2D.usedByComposite = hashtable.Get<bool>(StringConst.String_usedByComposite);
			tilemapCollider2D.offset = hashtable.Get<string>(StringConst.String_offset).ToVector2OrDefault();
		}
	}
}
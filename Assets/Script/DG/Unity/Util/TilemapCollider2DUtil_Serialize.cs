using System.Collections;
using UnityEngine.Tilemaps;

namespace DG
{
	public partial class TilemapCollider2DUtil
	{
		public static Hashtable GetSerializeHashtable(TilemapCollider2D tilemapCollider2D)
		{
			Hashtable hashtable = new Hashtable
			{
				[StringConst.STRING_MAXIMUM_TILE_CHANGE_COUNT] = tilemapCollider2D.maximumTileChangeCount,
				[StringConst.STRING_EXTRUSION_FACTOR] = tilemapCollider2D.extrusionFactor,
				[StringConst.STRING_IS_TRIGGER] = tilemapCollider2D.isTrigger,
				[StringConst.STRING_USE_BY_EFFECTOR] = tilemapCollider2D.usedByEffector,
				[StringConst.STRING_USED_BY_COMPOSITE] = tilemapCollider2D.usedByComposite,
				[StringConst.STRING_OFFSET] = tilemapCollider2D.offset.ToStringOrDefault()
			};
			hashtable.Trim();
			return hashtable;
		}

		public static void LoadSerializeHashtable(TilemapCollider2D tilemapCollider2D, Hashtable hashtable)
		{
			tilemapCollider2D.maximumTileChangeCount = hashtable.Get<uint>(StringConst.STRING_MAXIMUM_TILE_CHANGE_COUNT);
			tilemapCollider2D.extrusionFactor = hashtable.Get<float>(StringConst.STRING_EXTRUSION_FACTOR);
			tilemapCollider2D.isTrigger = hashtable.Get<bool>(StringConst.STRING_IS_TRIGGER);
			tilemapCollider2D.usedByEffector = hashtable.Get<bool>(StringConst.STRING_USE_BY_EFFECTOR);
			tilemapCollider2D.usedByComposite = hashtable.Get<bool>(StringConst.STRING_USED_BY_COMPOSITE);
			tilemapCollider2D.offset = hashtable.Get<string>(StringConst.STRING_OFFSET).ToVector2OrDefault();
		}
	}
}
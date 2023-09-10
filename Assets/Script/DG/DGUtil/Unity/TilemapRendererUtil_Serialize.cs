using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DG
{
	public partial class TilemapRendererUtil
	{
		public static Hashtable GetSerializeHashtable(TilemapRenderer tilemapRenderer)
		{
			Hashtable hashtable = new Hashtable
			{
				[StringConst.String_mode] = (int)tilemapRenderer.mode,
				[StringConst.String_detectChunkCullingBounds] = (int)tilemapRenderer.detectChunkCullingBounds,
				[StringConst.String_sortOrder] = (int)tilemapRenderer.sortOrder,
				[StringConst.String_sortingOrder] = tilemapRenderer.sortingOrder,
				[StringConst.String_maskInteraction] = (int)tilemapRenderer.maskInteraction
			};
			hashtable.Trim();
			return hashtable;
		}

		public static void LoadSerializeHashtable(TilemapRenderer tilemapRenderer, Hashtable hashtable)
		{
			tilemapRenderer.mode = hashtable.Get<int>(StringConst.String_mode).ToEnum<TilemapRenderer.Mode>();
			tilemapRenderer.detectChunkCullingBounds =
				hashtable.Get<int>(StringConst.String_detectChunkCullingBounds)
					.ToEnum<TilemapRenderer.DetectChunkCullingBounds>();
			tilemapRenderer.sortOrder = hashtable.Get<int>(StringConst.String_sortOrder).ToEnum<TilemapRenderer.SortOrder>();
			tilemapRenderer.sortingOrder = hashtable.Get<int>(StringConst.String_sortingOrder);
			tilemapRenderer.maskInteraction =
				hashtable.Get<int>(StringConst.String_maskInteraction).ToEnum<SpriteMaskInteraction>();
		}
	}
}
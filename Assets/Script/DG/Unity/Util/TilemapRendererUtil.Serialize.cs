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
                [StringConst.STRING_MODE] = (int)tilemapRenderer.mode,
                [StringConst.STRING_DETECT_CHUNK_CULLING_BOUNDS] = (int)tilemapRenderer.detectChunkCullingBounds,
                [StringConst.STRING_SORT_ORDER] = (int)tilemapRenderer.sortOrder,
                [StringConst.STRING_SORTING_ORDER] = tilemapRenderer.sortingOrder,
                [StringConst.STRING_MASK_INTERACTION] = (int)tilemapRenderer.maskInteraction
            };
            hashtable.Trim();
            return hashtable;
        }

        public static void LoadSerializeHashtable(TilemapRenderer tilemapRenderer, Hashtable hashtable)
        {
            tilemapRenderer.mode = hashtable.Get<int>(StringConst.STRING_MODE).ToEnum<TilemapRenderer.Mode>();
            tilemapRenderer.detectChunkCullingBounds =
                hashtable.Get<int>(StringConst.STRING_DETECT_CHUNK_CULLING_BOUNDS)
                    .ToEnum<TilemapRenderer.DetectChunkCullingBounds>();
            tilemapRenderer.sortOrder =
                hashtable.Get<int>(StringConst.STRING_SORT_ORDER).ToEnum<TilemapRenderer.SortOrder>();
            tilemapRenderer.sortingOrder = hashtable.Get<int>(StringConst.STRING_SORTING_ORDER);
            tilemapRenderer.maskInteraction =
                hashtable.Get<int>(StringConst.STRING_MASK_INTERACTION).ToEnum<SpriteMaskInteraction>();
        }
    }
}
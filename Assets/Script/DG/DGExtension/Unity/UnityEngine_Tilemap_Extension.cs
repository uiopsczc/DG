using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DG
{
	public static partial class UnityEngine_Tilemap_Extension
	{
		public static void SetTile(this Tilemap self, Vector3Int cellPos, TileBase tileBase, Hashtable tileDetailDict)
		{
			TilemapUtil.SetTile(self, cellPos, tileBase, tileDetailDict);
		}
	}
}


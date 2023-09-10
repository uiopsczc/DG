using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DG
{
	public partial class TilemapUtil
	{
		public static void SetTile(Tilemap tilemap, Vector3Int cellPos, TileBase tileBase, Hashtable tileDetailDict)
		{
			tilemap.SetTile(cellPos, tileBase);
			TileFlags tileFlags = tileDetailDict.Get<int>(StringConst.String_tileFlags).ToEnum<TileFlags>();
			tilemap.SetTileFlags(cellPos, tileFlags);

			tilemap.SetTransformMatrix(cellPos,
				tileDetailDict.Get<string>(StringConst.String_transformMatrix).ToMatrix4x4OrDefault(null, Matrix4x4.identity));
		}
	}
}
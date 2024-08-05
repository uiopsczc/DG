//using System.Collections;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif
//using UnityEngine;
//using UnityEngine.Tilemaps;
//
//namespace DG
//{
//	public static partial class TilemapUtil
//	{
//#if UNITY_EDITOR
//		public static Hashtable GetSerializeHashtable(Tilemap tilemap, Hashtable refIdHashtable = null)
//		{
//			Hashtable hashtable = new Hashtable
//			{
//				[StringConst.STRING_ANIMATION_FRAME_RATE] = tilemap.animationFrameRate,
//				[StringConst.STRING_COLOR] = tilemap.color.ToHtmlStringRGBAOrDefault(),
//				[StringConst.STRING_TILE_ANCHOR] = tilemap.tileAnchor.ToStringOrDefault(null, new Vector3(0.5f, 0.5f, 0)),
//				[StringConst.STRING_ORIENTATION] = (int)tilemap.orientation
//			};
//
//			Hashtable tileHashtable = new Hashtable();
//			Vector3Int size = tilemap.size;
//			Vector3Int origin = tilemap.origin;
//			hashtable[StringConst.STRING_SIZE] = size.ToStringOrDefault();
//			hashtable[StringConst.STRING_ORIGIN] = origin.ToStringOrDefault();
//			int checkCount = size.x * size.y * size.z;
//			for (int i = 0; i < checkCount; i++)
//			{
//				int offsetZ = i / (size.x * size.y);
//				int offsetY = (i - offsetZ * (size.x * size.y)) / size.x;
//				int offsetX = i - offsetZ * (size.x * size.y) - offsetY * size.x;
//
//				Vector3Int current = origin + new Vector3Int(offsetX, offsetY, offsetZ);
//				if (tilemap.HasTile(current))
//				{
//					Hashtable tileDetailHashtable = new Hashtable();
//
//					TileBase tileBase = tilemap.GetTile(current);
//					string assetPath = tileBase.GetAssetPath();
//					string guid = AssetDatabase.AssetPathToGUID(assetPath);
//					long refId = AssetPathRefManager.instance.GetRefIdByGuid(guid);
//					tileDetailHashtable[StringConst.STRING_TILE_BASE_REF_ID] = refId;
//					if (refIdHashtable != null)
//						refIdHashtable[refId] = true;
//
//					TileFlags tileFlags = tilemap.GetTileFlags(current);
//					tileDetailHashtable[StringConst.STRING_TILE_FLAGS] = (int)tileFlags;
//
//					tileDetailHashtable[StringConst.STRING_TRANSFORM_MATRIX] =
//						tilemap.GetTransformMatrix(current).ToStringOrDefault(null, Matrix4x4.identity);
//
//					tileHashtable[current.ToString()] = tileDetailHashtable;
//				}
//			}
//
//			hashtable[StringConst.STRING_TILE_HASHTABLE] = tileHashtable;
//			hashtable.Trim();
//			return hashtable;
//		}
//#endif
//
//		public static void LoadSerializeHashtable(this Tilemap self, Hashtable hashtable, ResLoad resLoad)
//		{
//			self.animationFrameRate = hashtable.Get<float>(StringConst.STRING_ANIMATION_FRAME_RATE);
//			self.color = hashtable.Get<string>(StringConst.STRING_COLOR).ToColorOrDefault();
//			self.tileAnchor = hashtable.Get<string>(StringConst.STRING_TILE_ANCHOR).ToVector3OrDefault(null, new Vector3(0.5f, 0.5f, 0));
//			self.orientation = hashtable.Get<int>(StringConst.STRING_ORIENTATION).ToEnum<Tilemap.Orientation>();
//
//			Vector3Int size = hashtable.Get<string>(StringConst.STRING_SIZE).ToVector3IntOrDefault();
//			Vector3Int origin = hashtable.Get<string>(StringConst.STRING_ORIGIN).ToVector3IntOrDefault();
//			self.size = size;
//			self.origin = origin;
//			Hashtable tileHashtable = hashtable.Get<Hashtable>(StringConst.STRING_TILE_HASHTABLE);
//
//
//			foreach (string cellPositionString in tileHashtable.Keys)
//			{
//				Vector3Int cellPos = cellPositionString.ToVector3().ToVector3Int();
//				Hashtable tileDetailHashtable = tileHashtable.Get<Hashtable>(cellPositionString);
//				long tileBaseRefId = tileDetailHashtable.Get<long>(StringConst.STRING_TILE_BASE_REF_ID);
//				string assetPath = tileBaseRefId.GetAssetPathByRefId();
//				resLoad.GetOrLoadAsset(assetPath, assetCat =>
//				{
//					TileBase tileBase = assetCat.Get<TileBase>();
//					SetTile(self, cellPos, tileBase, tileDetailHashtable);
//				}, null, null, self);
//			}
//		}
//	}
//}


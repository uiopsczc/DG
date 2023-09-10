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
//				[StringConst.String_animationFrameRate] = tilemap.animationFrameRate,
//				[StringConst.String_color] = tilemap.color.ToHtmlStringRGBAOrDefault(),
//				[StringConst.String_tileAnchor] = tilemap.tileAnchor.ToStringOrDefault(null, new Vector3(0.5f, 0.5f, 0)),
//				[StringConst.String_orientation] = (int)tilemap.orientation
//			};
//
//			Hashtable tileHashtable = new Hashtable();
//			Vector3Int size = tilemap.size;
//			Vector3Int origin = tilemap.origin;
//			hashtable[StringConst.String_size] = size.ToStringOrDefault();
//			hashtable[StringConst.String_origin] = origin.ToStringOrDefault();
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
//					tileDetailHashtable[StringConst.String_tileBase_ref_id] = refId;
//					if (refIdHashtable != null)
//						refIdHashtable[refId] = true;
//
//					TileFlags tileFlags = tilemap.GetTileFlags(current);
//					tileDetailHashtable[StringConst.String_tileFlags] = (int)tileFlags;
//
//					tileDetailHashtable[StringConst.String_transformMatrix] =
//						tilemap.GetTransformMatrix(current).ToStringOrDefault(null, Matrix4x4.identity);
//
//					tileHashtable[current.ToString()] = tileDetailHashtable;
//				}
//			}
//
//			hashtable[StringConst.String_tile_hashtable] = tileHashtable;
//			hashtable.Trim();
//			return hashtable;
//		}
//#endif
//
//		public static void LoadSerializeHashtable(this Tilemap self, Hashtable hashtable, ResLoad resLoad)
//		{
//			self.animationFrameRate = hashtable.Get<float>(StringConst.String_animationFrameRate);
//			self.color = hashtable.Get<string>(StringConst.String_color).ToColorOrDefault();
//			self.tileAnchor = hashtable.Get<string>(StringConst.String_tileAnchor).ToVector3OrDefault(null, new Vector3(0.5f, 0.5f, 0));
//			self.orientation = hashtable.Get<int>(StringConst.String_orientation).ToEnum<Tilemap.Orientation>();
//
//			Vector3Int size = hashtable.Get<string>(StringConst.String_size).ToVector3IntOrDefault();
//			Vector3Int origin = hashtable.Get<string>(StringConst.String_origin).ToVector3IntOrDefault();
//			self.size = size;
//			self.origin = origin;
//			Hashtable tileHashtable = hashtable.Get<Hashtable>(StringConst.String_tile_hashtable);
//
//
//			foreach (string cellPositionString in tileHashtable.Keys)
//			{
//				Vector3Int cellPos = cellPositionString.ToVector3().ToVector3Int();
//				Hashtable tileDetailHashtable = tileHashtable.Get<Hashtable>(cellPositionString);
//				long tileBaseRefId = tileDetailHashtable.Get<long>(StringConst.String_tileBase_ref_id);
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
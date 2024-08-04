using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DG
{
	public partial class UnityObjectUtil
	{
		public static void GetRefId(Object asset)
		{
		}

		public static string GetName(Object self)
		{
			return self == null ? StringConst.STRING_NULL : self.name;
		}

#if UNITY_EDITOR
		public static void AddObjectToAsset(Object asset, Object obj)
		{
			AssetDatabase.AddObjectToAsset(obj, asset);
		}

		public static void ClearLabels(Object asset)
		{
			AssetDatabase.ClearLabels(asset);
		}

		public static void SetLabels(Object asset, params string[] labels)
		{
			AssetDatabase.SetLabels(asset, labels);
		}

		public static string[] GetLabels(Object asset)
		{
			return asset.GetLabels();
		}

		public static bool IsAsset(Object self)
		{
			return AssetDatabase.Contains(self);
		}

		public static string GetAssetPath(Object asset)
		{
			if (!asset.IsMainAsset())
				return AssetDatabase.GetAssetPath(asset) + StringConst.STRING_COLON + asset.name;
			return AssetDatabase.GetAssetPath(asset);
		}

		public static void CreateAssetAtPath(Object asset, string path)
		{
			AssetDatabase.CreateAsset(asset, path);
		}

		public static void ExtractAssetAtPath(Object asset, string path)
		{
			AssetDatabase.ExtractAsset(asset, path);
		}


		public static bool IsForeignAsset(Object asset)
		{
			return AssetDatabase.IsForeignAsset(asset);
		}

		public static bool IsNativeAsset(Object asset)
		{
			return AssetDatabase.IsNativeAsset(asset);
		}

		public static bool IsMainAsset(Object asset)
		{
			return AssetDatabase.IsMainAsset(asset);
		}

		public static bool IsSubAsset(Object asset)
		{
			return AssetDatabase.IsSubAsset(asset);
		}

		public static bool OpenAsset(Object asset, int lineNumber = -1)
		{
			return AssetDatabase.OpenAsset(asset, lineNumber);
		}

		public static bool OpenAsset(Object[] assets)
		{
			return AssetDatabase.OpenAsset(assets);
		}

		public static string GetGUID(Object asset)
		{
			AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out string guid, out long localId);
			return guid;
		}


		public static long GetLocalFileId(Object asset)
		{
			AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out string guid, out long localId);
			return localId;
		}

		public static Texture2D GetAssetPreview(Object asset)
		{
			return AssetPreview.GetAssetPreview(asset);
		}


		public static Texture2D GetMiniThumbnail(Object asset)
		{
			return AssetPreview.GetMiniThumbnail(asset);
		}
#endif
	}
}
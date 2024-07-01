#if UNITY_EDITOR
#endif

namespace DG
{
	public static partial class BuildConst
	{
		#region AssetPathMap

		public const string ASSET_PATH_MAP_FILE_NAME = "AssetPathMap.bytes";

		#endregion

		public static string ASSETS_PACKAGE_ROOT = "Assets/" + ASSETS_PACKAGE_FOLDER_NAME + "/";
		public static string LUA_ROOT = "Assets/" + "Lua" + "/";


		#region Mainifest

		public const string MANIFEST_BOUNDLE_PATH = ASSET_BUNDLE_FOLDER_NAME;
		public const string MANIFEST_PATH = "AssetBundleManifest";

		#endregion

		#region AssetsPackage

		public const string ASSETS_PACKAGE_FOLDER_NAME = "AssetsPackage";

		#endregion

		#region AssetBundle

		public const string ASSET_BUNDLE_SUFFIX = ".ab";
		public const string ASSET_BUNDLE_FOLDER_NAME = "AssetBundle";

		#endregion

		#region ResVersion

		public const string RES_VERSION_DEFAULT = "1.0.00000";
		public const string RES_VERSION_FILE_NAME = "ResVersion.bytes";
		public const string RES_VERSION_FILE_PATH = "Assets/" + RES_VERSION_FILE_NAME;

		#endregion

		#region AssetBundleMap

		public const string ASSET_BUNDLE_MAP_FILE_NAME = "AssetBundleMap.bytes";
		public const string ASSET_BUNDLE_MAP_FILE_PATH = "Assets/" + ASSET_BUNDLE_MAP_FILE_NAME;

		#endregion

		#region Lua

		public const string LUA_BUNDLE_PREFIX_NAME = "@lua_"; //小写，assetBundle_path全部都是小写的
		public const string LUA_SUFFIX = ".lua.txt";

		public const string LUA_PATH_MAP_FILE_NAME = "LuaPathMap.bytes";

		#endregion
	}
}
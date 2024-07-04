namespace DG
{
	public class AssetPackageUtil
	{
		public static string AssetsPackagePathToAssetsPath(string assetPackagePath)
		{
			return assetPackagePath.WithRootPath(BuildConst.ASSETS_PACKAGE_ROOT);
		}

		public static bool IsAssetsPackagePath(string assetPath)
		{
			return assetPath.IndexOf(BuildConst.ASSETS_PACKAGE_ROOT) != -1;
		}

		public static string AssetsPathToAssetsPackagePath(string assetPath)
		{
			int index = assetPath.IndexEndOf(BuildConst.ASSETS_PACKAGE_ROOT);
			if (index != -1)
				return assetPath.Substring(index + 1);
			DGLog.Error("Asset path is not a package path!");
			return assetPath;
		}
	}
}
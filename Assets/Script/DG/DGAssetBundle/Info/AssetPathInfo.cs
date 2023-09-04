using DG;

namespace DG
{
	public class AssetPathInfo
	{
		public string mainAssetPath;
		public string subAssetPath;

		public AssetPathInfo(string path)
		{
			var paths = path.Split(CharConst.Char_Colon);
			mainAssetPath = paths[0];
			if (paths.Length > 1)
				subAssetPath = paths[1];
		}
	}
}
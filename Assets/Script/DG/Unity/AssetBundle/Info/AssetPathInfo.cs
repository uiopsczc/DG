namespace DG
{
    public class AssetPathInfo
    {
        public string mainAssetPath;
        public string subAssetPath;

        public AssetPathInfo(string path)
        {
            var paths = path.Split(CharConst.CHAR_COLON);
            mainAssetPath = paths[0];
            subAssetPath = paths.Length > 1 ? paths[1] : null;
        }
    }
}
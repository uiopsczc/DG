using UnityEditor;

#if UNITY_EDITOR
namespace DG
{
    public static class ScriptableObjectTest
    {
        public static void CreateInstance()
        {
            //      ScriptableObjectUtil.CreateAsset<TestScriptableObject1>("Assets/ff.asset", (asset) =>
            //      {
            //        asset.name = "chen";
            //        asset.indexes.Add(new TestScriptableObjectAA("aa", 4));
            //        asset.indexes.Add(new TestScriptableObjectAA("bb", 5));
            //        asset.indexes.Add(new TestScriptableObjectAA("cc", 6));
            //      });

            ScriptableObjectUtil.CreateAsset<TestScriptableObject2Impl>("Assets/ff.asset", asset =>
            {
                asset.name = "chen";
                asset.indexes = new TestScriptableObjectBB<string, int>("aa", 6);
            });
        }

        public static void LoadAsset()
        {
            var obj = AssetDatabase.LoadAssetAtPath<TestScriptableObject1>("Assets/ff.asset");
            DGLog.Info(obj.indexes, obj.name);
        }
    }
}
#endif
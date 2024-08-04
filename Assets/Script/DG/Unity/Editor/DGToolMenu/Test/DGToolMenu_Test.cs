using System.Collections;
using UnityEditor;
using UnityEngine;

namespace DG
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class DGToolMenu : MonoBehaviour
	{
		private static int i;

		[MenuItem(DGToolConst.Menu_Root + "Test")]
		public static void Test()
		{
			//      ScriptableObjectTest.CreateInstance();
			//      EditorWindow.GetWindow<AnimationTimelinableTestEditorWindow>();
			//      EditorWindow.GetWindow<MountTimelinableTestEditorWindow>();
			// EditorWindow.GetWindow<SkinnedMeshRendererTimelinableTestEditorWindow>();
			//      List<AC> list = new List<AC>();
			//      list.Add(new AC("chen"));
			//      list.Add(list[0].Clone());
			//      list[0].name = "quan";
			//      LogCat.log(list);
			//      ScriptableObjectTest.CreateInstance();
			//    var obj = Selection.activeObject;
			//    LogCat.log(AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(obj)));
			//    LogCat.log(AssetDatabase.GUIDToAssetPath(AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(obj))));
		}


		[UnityEditor.MenuItem(DGToolConst.Menu_Root + "Test2")]
		public static void Test2()
		{
//      LogCat.log(Application.dataPath);
			ShellUtilTest.Test();
			//      Dictionary<string,string> dict = new Dictionary<string, string>();
			//      dict["kk"] = "ff";
			//      LogCat.log("before:", dict.GetHashCode());
			//      dict["hh"] = "gg";
			//      LogCat.log("after:", dict.GetHashCode());
		}


		public static void T1()
		{
			DGLog.Info("t1");
		}

		public static void T2()
		{
			DGLog.Info("t2");
		}


		[UnityEditor.MenuItem(DGToolConst.Menu_Root + "TestEditorWindow")]
		public static void TestEditor()
		{
			EditorWindow.GetWindow<TestEditorWindow>(false, "TestEditorWindow").minSize = new Vector2(720f, 480f);
		}
	}
}
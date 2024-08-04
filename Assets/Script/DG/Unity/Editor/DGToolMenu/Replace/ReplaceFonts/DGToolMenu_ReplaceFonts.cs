using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace DG
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class DGToolMenu
	{
		public static string fontToReplacePath = Application.dataPath; //Application.dataPath+"/UI"

		public static Dictionary<string, Dictionary<string, string>> fontToReplaceDict
		{
			get
			{
				var result = new Dictionary<string, Dictionary<string, string>>();
				foreach (var fontToReplaceDict in _fontToReplaceDictList)
					result[fontToReplaceDict["old_fileId"]] = fontToReplaceDict;
				return result;
			}
		}

		private static ValueDictList<string, string> _fontToReplaceDictList = new()
		{
			new Dictionary<string, string>
			{
				{"old_guid", ""}, {"old_fileId", ""}, {"old_type", "type:0"}, {"new_guid:", ""},
				{"new_fileId:", ""}, {"new_type", "type:3"}
			},
			new Dictionary<string, string>
			{
				{"old_guid", ""}, {"old_fileId", ""}, {"old_type", "type:0"}, {"new_guid:", ""},
				{"new_fileId:", ""}, {"new_type", "type:3"}
			},
		};


		[MenuItem(DGToolConst.Menu_Root + "Relpace/Relpace Fonts")]
		public static void RelpaceFonts()
		{
			var rootPrefabPath = fontToReplacePath;
			if (Directory.Exists(rootPrefabPath))
			{
				string[] allPrefabPathes =
					Directory.GetFiles(rootPrefabPath, "*.prefab", SearchOption.AllDirectories);
				foreach (string prefabPath in allPrefabPathes)
				{
					bool isChanged = false;
					var lines = File.ReadAllLines(prefabPath);
					for (int i = 0; i < lines.Length; i++)
					{
						var line = lines[i];
						string matchedLineContent = MetaConst.FONT_REGEX.Match(line).Value;
						if (!matchedLineContent.IsNullOrEmpty())
						{
							string oldFiledId = MetaConst.FILE_ID_REGEX.Match(matchedLineContent).Value;
							string oldGUID = MetaConst.GUID_REGEX.Match(matchedLineContent).Value;

							if (fontToReplaceDict.ContainsKey(oldFiledId) &&
							    oldGUID.Equals(fontToReplaceDict[oldFiledId]["old_guid"]))
							{
								isChanged = true;
								var dict = fontToReplaceDict[oldFiledId];
								lines[i] = Regex.Replace(lines[i], oldFiledId, dict["new_fileId"])
									.Replace(oldGUID, dict["new_guid"]).Replace(dict["old_type"], dict["new_type"]);
							}
						}
					}

					if (isChanged)
						File.WriteAllLines(prefabPath, lines);
				}

				AssetDatabase.Refresh();
				DGEditorUtility.DisplayDialog("Relpace Fonts finished");
			}
		}
	}
}
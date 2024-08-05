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
        public static string spriteToReplacePath = Application.dataPath; //Application.dataPath+"/UI"

        public static Dictionary<string, Dictionary<string, string>> spriteToReplaceDict
        {
            get
            {
                var result = new Dictionary<string, Dictionary<string, string>>();
                foreach (var spriteToReplaceDict in _spriteToReplaceDictList)
                    result[spriteToReplaceDict["old_fileId"]] = spriteToReplaceDict;
                return result;
            }
        }

        private static ValueDictList<string, string> _spriteToReplaceDictList = new()
        {
            new Dictionary<string, string>
                { { "old_guid:", "" }, { "old_fileId:", "" }, { "new_guid:", "" }, { "new_fileId:", "" } },
            new Dictionary<string, string>
                { { "old_guid:", "" }, { "old_fileId:", "" }, { "new_guid:", "" }, { "new_fileId:", "" } },
        };

        [MenuItem(DGToolConst.Menu_Root + "Relpace/Relpace Sprites")]
        public static void RelpaceSprites()
        {
            var rootPrefabPath = spriteToReplacePath;
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
                        string matchedLineContent = MetaConst.SPRITE_REGEX.Match(line).Value;
                        if (!matchedLineContent.IsNullOrEmpty())
                        {
                            string oldFiledId = MetaConst.FILE_ID_REGEX.Match(matchedLineContent).Value;
                            string oldGUID = MetaConst.GUID_REGEX.Match(matchedLineContent).Value;

                            if (spriteToReplaceDict.ContainsKey(oldFiledId) &&
                                oldGUID.Equals(spriteToReplaceDict[oldFiledId]["old_guid"]))
                            {
                                isChanged = true;
                                var dict = spriteToReplaceDict[oldFiledId];
                                lines[i] = Regex.Replace(lines[i], oldFiledId, dict["new_fileId"])
                                    .Replace(oldGUID, dict["new_guid"]);
                            }
                        }
                    }

                    if (isChanged)
                        File.WriteAllLines(prefabPath, lines);
                }

                AssetDatabase.Refresh();
                DGEditorUtility.DisplayDialog("Relpace Sprites finished");
            }
        }
    }
}
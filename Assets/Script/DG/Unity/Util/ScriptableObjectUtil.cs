using UnityEngine;
#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
#endif

namespace DG
{
    public class ScriptableObjectUtil
    {
#if UNITY_EDITOR

        public static T CreateAsset<T>(string path, Action<T> onCreateCallback = null) where T : ScriptableObject
        {
            var asset = ScriptableObject.CreateInstance<T>();
            path = path.WithoutRootPath(FilePathConst.PROJECT_PATH);
            var dir = path.DirPath();
            if (!dir.IsNullOrEmpty())
                StdioUtil.CreateDirectoryIfNotExist(dir);
            var fileExtensionName = Path.GetExtension(path);
            if (!StringConst.STRING_ASSET_EXTENSION.Equals(fileExtensionName))
                path = path.Replace(fileExtensionName, StringConst.STRING_ASSET_EXTENSION);
            onCreateCallback?.Invoke(asset);

            EditorUtility.SetDirty(asset);
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return asset;
        }


#endif
    }
}
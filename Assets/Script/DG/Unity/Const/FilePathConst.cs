using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DG
{
    public static class FilePathConst
    {
        #region PROJECT_PATH

        private static string _PROJECT_PATH;

        public static string PROJECT_PATH => _PROJECT_PATH ??
                                             (_PROJECT_PATH = Application.dataPath.Replace(StringConst.STRING_ASSETS,
                                                 StringConst.STRING_EMPTY));

        #endregion

        #region dataPath

        private static string _DATA_PATH;
        public static string DATA_PATH = _DATA_PATH ?? (_DATA_PATH = Application.dataPath + StringConst.STRING_SLASH);

        #endregion

        #region AssetPath

        public static string ASSETS_PATH = DATA_PATH;

        #endregion

        #region streamingAssetsPath

        private static string _STREAMING_ASSETS_PATH;

        public static string STREAMING_ASSETS_PATH = _STREAMING_ASSETS_PATH ??
                                                     (_STREAMING_ASSETS_PATH =
                                                         Application.streamingAssetsPath + StringConst.STRING_SLASH);

        #endregion

        #region persistentDataPath

        private static string _PERSISTENT_DATA_PATH;

        public static string PERSISTENT_DATA_PATH = _PERSISTENT_DATA_PATH ??
                                                    (_PERSISTENT_DATA_PATH =
                                                        Application.persistentDataPath + StringConst.STRING_SLASH);

        #endregion

        #region AssetBundlePath

        private static string _ASSET_BUNDLES_PATH;

        public static string ASSET_BUNDLES_PATH = ASSET_BUNDLES_PATH ?? (ASSET_BUNDLES_PATH =
                                                      STREAMING_ASSETS_PATH + BuildConst.ASSET_BUNDLE_FOLDER_NAME +
                                                      StringConst.STRING_SLASH);

        #endregion

        #region RESOURCES_PATH

        private static string _RESOURCES_PATH;

        public static string RESOURCES_PATH = _RESOURCES_PATH ??
                                              (_RESOURCES_PATH =
                                                  ASSETS_PATH + StringConst.STRING_RESOURCES +
                                                  StringConst.STRING_SLASH);

        public const string RESOURCES_FLAG =
            StringConst.STRING_SLASH + StringConst.STRING_RESOURCES + StringConst.STRING_SLASH;

        #endregion

        #region SPRITES_PATH

        private static string _SPRITES_PATH;
        public static string SPRITES_PATH = RESOURCES_PATH + StringConst.STRING_SPRITES + StringConst.STRING_SLASH;

        #endregion


        #region assetBundlesMainfest

        private static string _ASSET_BUNDLES_MANIFEST;

        public static string ASSET_BUNDLES_MANIFEST =
            _ASSET_BUNDLES_MANIFEST ?? (_ASSET_BUNDLES_MANIFEST = ASSET_BUNDLES_PATH + StringConst.STRING_MANIFEST);

        #endregion

        #region EXES_PATH 执行路径

        private static string _EXES_PATH;

        public static string EXES_PATH =
            _EXES_PATH ?? (_EXES_PATH = ASSETS_PATH + StringConst.STRING_EXES + StringConst.STRING_SLASH);

        #endregion


        #region EXTERNAL_SCRIPTS_PATH 脚本路径

        private static string _EXTERNAL_SCRIPTS_PATH;

        public static string EXTERNAL_SCRIPTS_PATH =
            PROJECT_PATH + StringConst.STRING_EXTERNAL_SCRIPTS + StringConst.STRING_SLASH;

        #endregion

        #region ASSET_BUNDLES_BUILD_OUTPUT_PATH

        private static string __ASSET_BUNDLES_BUILD_OUTPUT_PATH;

        private static string _ASSET_BUNDLES_BUILD_OUTPUT_PATH =
            __ASSET_BUNDLES_BUILD_OUTPUT_PATH ?? (__ASSET_BUNDLES_BUILD_OUTPUT_PATH =
                Path.Combine(PROJECT_PATH, BuildConst.ASSET_BUNDLE_FOLDER_NAME));

        public static string ASSET_BUNDLES_BUILD_OUTPUT_PATH
        {
            get
            {
                //TODO 等StdioUtil实现后反注释
//				StdioUtil.CreateDirectoryIfNotExist(_ASSET_BUNDLES_BUILD_OUTPUT_PATH);
                return _ASSET_BUNDLES_BUILD_OUTPUT_PATH;
            }
        }

        #endregion


        #region PathBases  Unity所有资源脚本保存数据的路径

        public static List<string> ROOT_PATH_LIST
        {
            get
            {
                var result = new List<string>
                {
                    EXTERNAL_PATH,
                    EXES_PATH,
                    EXTERNAL_SCRIPTS_PATH,
                    ASSET_BUNDLES_PATH,
                    SPRITES_PATH,
                    RESOURCES_PATH
                };
                //由外而内
                return result;
            }
        }

        #endregion

        public static string GetPathStartsWithRelativePath(string path, string relativePath)
        {
            path = GetPathRelativeTo(path, relativePath);
            path = relativePath + path;
            return path;
        }

        public static string GetPathRelativeTo(string path, string relativePath)
        {
            var index = path.IndexEndOf(relativePath);
            if (index != -1)
                path = path.Substring(index + 1);
            return path;
        }

        #region PERSISTENT_ASSET_BUNDLE_ROOT

        public static string _PERSISTENT_ASSET_BUNDLE_ROOT;

        public static string PERSISTENT_ASSET_BUNDLE_ROOT =
            _PERSISTENT_ASSET_BUNDLE_ROOT ?? (_PERSISTENT_ASSET_BUNDLE_ROOT =
                PERSISTENT_DATA_PATH + BuildConst.ASSET_BUNDLE_FOLDER_NAME +
                StringConst.STRING_SLASH);

        #endregion

        public const string EDITOR_ASSETS_PATH = "Assets/Editor/EditorAssets/";

        #region EXTERNAL_PATH Unity外部路径

        public static string EXTERNAL_PATH
        {
            get
            {
                var platform = Application.platform;
                switch (platform)
                {
                    case RuntimePlatform.WindowsEditor:
                        return ASSETS_PATH + "Patch/";
                    case RuntimePlatform.IPhonePlayer:
                    case RuntimePlatform.Android:
                        return PERSISTENT_DATA_PATH;
                    default:
                        return ASSETS_PATH + "Patch/";
                }
            }
        }

        #endregion

        #region EXCELS_PATH

        public static string EXCELS_PATH = Application.dataPath + "/Excels/";
        public static string EXCEL_ASSETS_PATH = RESOURCES_PATH + "data/excel_asset/";

        #endregion
    }
}
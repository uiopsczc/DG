using UnityEngine;

namespace DG
{
    public static class UnityEngine_Object_Extension
    {
        public static void Destroy(this Object self)
        {
            UnityObjectUtil.Destroy(self);
        }

        public static bool IsNull(this Object self)
        {
            return UnityObjectUtil.IsNull(self);
        }

        public static void GetRefId(this Object self)
        {
            UnityObjectUtil.GetRefId(self);
        }

        public static string GetName(this Object self)
        {
            return UnityObjectUtil.GetName(self);
        }

#if UNITY_EDITOR
        public static void AddObjectToAsset(this Object self, Object obj)
        {
            UnityObjectUtil.AddObjectToAsset(self, obj);
        }

        public static void ClearLabels(this Object self)
        {
            UnityObjectUtil.ClearLabels(self);
        }

        public static void SetLabels(this Object self, params string[] labels)
        {
            UnityObjectUtil.SetLabels(self, labels);
        }

        public static string[] GetLabels(this Object self)
        {
            return UnityObjectUtil.GetLabels(self);
        }

        public static bool IsAsset(this Object self)
        {
            return UnityObjectUtil.IsAsset(self);
        }

        public static string GetAssetPath(this Object self)
        {
            return UnityObjectUtil.GetAssetPath(self);
        }

        public static void CreateAssetAtPath(this Object self, string path)
        {
            UnityObjectUtil.CreateAssetAtPath(self, path);
        }

        public static void ExtractAssetAtPath(this Object self, string path)
        {
            UnityObjectUtil.ExtractAssetAtPath(self, path);
        }


        public static bool IsForeignAsset(this Object self)
        {
            return UnityObjectUtil.IsForeignAsset(self);
        }

        public static bool IsNativeAsset(this Object self)
        {
            return UnityObjectUtil.IsNativeAsset(self);
        }

        public static bool IsMainAsset(this Object self)
        {
            return UnityObjectUtil.IsMainAsset(self);
        }

        public static bool IsSubAsset(this Object self)
        {
            return UnityObjectUtil.IsSubAsset(self);
        }

        public static bool OpenAsset(this Object self, int lineNumber = -1)
        {
            return UnityObjectUtil.OpenAsset(self, lineNumber);
        }

        public static bool OpenAsset(this Object[] self)
        {
            return UnityObjectUtil.OpenAsset(self);
        }

        public static string GetGUID(this Object self)
        {
            return UnityObjectUtil.GetGUID(self);
        }


        public static long GetLocalFileId(this Object self)
        {
            return UnityObjectUtil.GetLocalFileId(self);
        }

        public static Texture2D GetAssetPreview(this Object self)
        {
            return UnityObjectUtil.GetAssetPreview(self);
        }


        public static Texture2D GetMiniThumbnail(this Object self)
        {
            return UnityObjectUtil.GetMiniThumbnail(self);
        }
#endif
    }
}
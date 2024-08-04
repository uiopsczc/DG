using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DG
{
    public static partial class System_Type_Extension
    {
#if UNITY_EDITOR
        public static Texture2D GetMiniTypeThumbnail(this Type self)
        {
            return AssetPreview.GetMiniTypeThumbnail(self);
        }
#endif
    }
}
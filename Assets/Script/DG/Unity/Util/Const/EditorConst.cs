#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace DG
{
    public static class EditorConst
    {
        public static float SINGLE_LINE_HEIGHT = EditorGUIUtility.singleLineHeight;

        public static Texture2D WHITE_TEXTURE => EditorGUIUtility.whiteTexture;
    }
}
#endif
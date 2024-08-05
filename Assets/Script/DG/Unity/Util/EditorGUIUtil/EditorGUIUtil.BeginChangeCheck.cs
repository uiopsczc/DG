#if UNITY_EDITOR
using UnityEditor;

namespace DG
{
    public partial class EditorGUIUtil
    {
        public static bool isChanged => EditorGUI.EndChangeCheck();

        public static EditorGUIBeginChangeCheckScope BeginChangeCheck()
        {
            return new EditorGUIBeginChangeCheckScope();
        }
    }
}
#endif
#if UNITY_EDITOR
using UnityEditorInternal;

namespace DG
{
    public static class UnityEditorInternal_ReorderableList_Extension
    {
        public static void DrawGUI(this ReorderableList self, GUIToggleTween toggleTween, string title)
        {
            ReorderableListUtil.DrawGUI(self, toggleTween, title);
        }

        public static void SetElementHeight(this ReorderableList reorderableList, float element_height)
        {
            ReorderableListUtil.SetElementHeight(reorderableList, element_height);
        }
    }
}
#endif
#if UNITY_EDITOR
using System;
using UnityEditor;

namespace DG
{
    public class EditorGUIBeginToggleGroupScope : IDisposable
    {
        public bool toggle { get; set; }

        public EditorGUIBeginToggleGroupScope(bool isToggle, string name = StringConst.STRING_EMPTY)
        {
            toggle = EditorGUILayout.BeginToggleGroup(name, isToggle);
        }


        public void Dispose()
        {
            EditorGUILayout.EndToggleGroup();
        }
    }
}
#endif
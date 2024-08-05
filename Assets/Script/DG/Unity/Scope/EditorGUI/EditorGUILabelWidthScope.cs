#if UNITY_EDITOR
using System;
using UnityEditor;

namespace DG
{
    public class EditorGUILabelWidthScope : IDisposable
    {
        private readonly float _preLabelWidth;

        public EditorGUILabelWidthScope(float labelWidth)
        {
            _preLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = labelWidth;
        }

        public void Dispose()
        {
            EditorGUIUtility.labelWidth = _preLabelWidth;
        }
    }
}
#endif
#if UNITY_EDITOR
using System;
using UnityEditor;

namespace DG
{
    public class EditorGUIBeginChangeCheckScope : IDisposable
    {
        private bool _isEndChangeCheck;
        private bool _isChanged;


        public EditorGUIBeginChangeCheckScope()
        {
            EditorGUI.BeginChangeCheck();
        }

        public bool IsChanged
        {
            get
            {
                if (_isEndChangeCheck) return _isChanged;
                _isChanged = EditorGUI.EndChangeCheck();
                _isEndChangeCheck = true;

                return _isChanged;
            }
        }

        public void Dispose()
        {
            if (_isEndChangeCheck)
                return;
            _isChanged = EditorGUI.EndChangeCheck();
            _isEndChangeCheck = true;
        }
    }
}
#endif
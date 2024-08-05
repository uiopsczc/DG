#if UNITY_EDITOR
using System;
using UnityEditor;

namespace DG
{
    public class HandlesBeginGUIScope : IDisposable
    {
        public HandlesBeginGUIScope()
        {
            Handles.BeginGUI();
        }

        public void Dispose()
        {
            Handles.EndGUI();
        }
    }
}
#endif
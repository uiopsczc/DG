#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace DG
{
    public class DGEditorUtility
    {
        public static void DisplayDialog(string message, string copyContent = null)
        {
            EditorUtility.DisplayDialog("", message, "确定");
            DGLog.Info(message);
            if (copyContent.IsNullOrWhiteSpace())
                GUIUtility.systemCopyBuffer = copyContent;
        }
    }
}
#endif
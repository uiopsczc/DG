#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace DG
{
    public class EditorMessageBox : EditorWindow
    {
        public string messageTitle;
        public string content;

        public string button1Text;
        public string button2Text;

        public Action onButton1Callback;
        public Action onButton2Callback;
        public Action onCancelCallback;


        void OnGUI()
        {
            EditorGUILayout.Space(10);

            // tips
            if (!string.IsNullOrEmpty(messageTitle))
            {
                EditorGUILayout.LabelField(messageTitle, GUIStyleConst.LABLE_BOLD_MIDDLE_CENTER_STYLE);
                EditorGUILayout.Space(10);
            }

            EditorGUILayout.LabelField(content, GUIStyleConst.LABLE_MIDDLE_CENTER_STYLE);

            GUILayout.FlexibleSpace();
            // buttons
            using (new GUILayoutBeginHorizontalScope())
            {
                GUILayout.FlexibleSpace();
                if (!string.IsNullOrEmpty(button1Text))
                {
                    if (GUILayout.Button(button1Text, GUILayout.Width(64)))
                    {
                        onButton1Callback?.Invoke();
                        onButton1Callback = null;
                        Close();
                    }
                }

                if (!string.IsNullOrEmpty(button2Text))
                {
                    if (GUILayout.Button(button2Text, GUILayout.Width(64)))
                    {
                        onButton2Callback?.Invoke();
                        onButton2Callback = null;
                        Close();
                    }
                }
            }
        }

        void OnDestroy()
        {
            onCancelCallback?.Invoke();
        }
    }
}
#endif
#if UNITY_EDITOR
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace DG
{
    public static class EditorMessageBoxUtil
    {
        public static EditorMessageBox Show(string title, string content, string button1Text,
            Action onButton1Callback = null, string button2Text = null, Action onButton2Callback = null,
            Action onCancelCallback = null)
        {
            EditorMessageBox editorMessageBox = EditorWindow.CreateWindow<EditorMessageBox>();
            editorMessageBox.minSize = new Vector2(EditorMessageBoxConst.WIDTH, EditorMessageBoxConst.HEIGHT);
            editorMessageBox.maxSize = editorMessageBox.minSize;
            editorMessageBox.messageTitle = title;
            editorMessageBox.content = content;

            editorMessageBox.button1Text = button1Text;
            editorMessageBox.onButton1Callback = onButton1Callback;

            editorMessageBox.button2Text = button2Text;
            editorMessageBox.onButton2Callback = onButton2Callback;

            editorMessageBox.onCancelCallback = onCancelCallback;

            editorMessageBox.Show();

            return editorMessageBox;
        }

        public static EditorMessageBox FadeShow(string title, string content, float duration = 1f)
        {
            var editorMessageBox = Show(title, content, null);
            editorMessageBox.minSize = new Vector2(EditorMessageBoxConst.WIDTH, EditorMessageBoxConst.HEIGHT / 2);
            editorMessageBox.maxSize = editorMessageBox.minSize;
            PausableCoroutineManager.instance.StartCoroutine(_IEFade(editorMessageBox, duration), editorMessageBox);
            return editorMessageBox;
        }

        private static IEnumerator _IEFade(EditorMessageBox editorMessageBox, float duration)
        {
            yield return new WaitForSeconds(duration);
            editorMessageBox.Close();
        }
    }
}
#endif
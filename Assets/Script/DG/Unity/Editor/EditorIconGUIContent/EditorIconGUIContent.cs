#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DG
{
    public class EditorIconGUIContent
    {
        static Dictionary<string, GUIContent> _name2IconGUIContentCache = new();

        public static int count => _name2IconGUIContentCache.Count;

        public static GUIContent CUSTOM_GUICONTENT => Get("CustomContent");


        public static GUIContent Get(string name, string text, string tips)
        {
            if (_name2IconGUIContentCache.TryGetValue(name, out var value))
                return value;
            GUIContent guiContent = new GUIContent(text, EditorIconTexture.GetCustom(name), tips);
            _name2IconGUIContentCache[name] = guiContent;
            return guiContent;
        }

        public static GUIContent Get(string name, string text)
        {
            if (_name2IconGUIContentCache.TryGetValue(name, out var value))
                return value;
            GUIContent guiContent = new GUIContent(text, EditorIconTexture.GetCustom(name));
            _name2IconGUIContentCache[name] = guiContent;
            return guiContent;
        }

        public static GUIContent Get(string name)
        {
            if (_name2IconGUIContentCache.TryGetValue(name, out var value))
                return value;
            GUIContent guiContent = new GUIContent(EditorIconTexture.GetCustom(name));
            _name2IconGUIContentCache.Add(name, guiContent);
            return guiContent;
        }

        public static GUIContent Get(EEditorIconTextureType editorIconTextureType)
        {
            if (_name2IconGUIContentCache.ContainsKey(editorIconTextureType.ToString()))
                return _name2IconGUIContentCache[editorIconTextureType.ToString()];
            GUIContent guiContent = new GUIContent(EditorIconTexture.GetSystem(editorIconTextureType));
            _name2IconGUIContentCache.Add(editorIconTextureType.ToString(), guiContent);
            return guiContent;
        }

        public static GUIContent GetSystem(EEditorIconGUIContentType editorIconGUIContentType)
        {
            string name = EditorIconGUIContentConst.IconGUIContentNames[(int)editorIconGUIContentType];
            if (_name2IconGUIContentCache.TryGetValue(name, out var system))
                return system;

            GUIContent guiContent = EditorGUIUtility.IconContent(name);
            _name2IconGUIContentCache[name] = guiContent;
            return guiContent;
        }
    }
}
#endif
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DG
{
	public class EditorIconGUIContent
	{
		static Dictionary<string, GUIContent> _iconGUIContentCacheDict = new();

		public static int count => _iconGUIContentCacheDict.Count;

		public static GUIContent CUSTOM_GUICONTENT => Get("CustomContent");


		public static GUIContent Get(string name, string text, string tips)
		{
			if (_iconGUIContentCacheDict.TryGetValue(name, out var value))
				return value;
			GUIContent guiContent = new GUIContent(text, EditorIconTexture.GetCustom(name), tips);
			_iconGUIContentCacheDict[name] = guiContent;
			return guiContent;
		}

		public static GUIContent Get(string name, string text)
		{
			if (_iconGUIContentCacheDict.TryGetValue(name, out var value))
				return value;
			GUIContent guiContent = new GUIContent(text, EditorIconTexture.GetCustom(name));
			_iconGUIContentCacheDict[name] = guiContent;
			return guiContent;
		}

		public static GUIContent Get(string name)
		{
			if (_iconGUIContentCacheDict.TryGetValue(name, out var value))
				return value;
			GUIContent guiContent = new GUIContent(EditorIconTexture.GetCustom(name));
			_iconGUIContentCacheDict.Add(name, guiContent);
			return guiContent;
		}

		public static GUIContent Get(EEditorIconTextureType editorIconTextureType)
		{
			if (_iconGUIContentCacheDict.ContainsKey(editorIconTextureType.ToString()))
				return _iconGUIContentCacheDict[editorIconTextureType.ToString()];
			GUIContent guiContent = new GUIContent(EditorIconTexture.GetSystem(editorIconTextureType));
			_iconGUIContentCacheDict.Add(editorIconTextureType.ToString(), guiContent);
			return guiContent;
		}

		public static GUIContent GetSystem(EEditorIconGUIContentType editorIconGUIContentType)
		{
			string name = EditorIconGUIContentConst.IconGUIContentNames[(int) editorIconGUIContentType];
			if (_iconGUIContentCacheDict.TryGetValue(name, out var system))
				return system;

			GUIContent guiContent = EditorGUIUtility.IconContent(name);
			_iconGUIContentCacheDict[name] = guiContent;
			return guiContent;
		}
	}
}
#endif
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DG
{
	public class EditorIconTexture
	{
		static Dictionary<string, Texture2D> _iconTextureCacheDict = new();


		public static int count => _iconTextureCacheDict.Count;

		public static Texture2D Get(string name)
		{
			if (_iconTextureCacheDict.TryGetValue(name, out var value))
				return value;

			Texture2D texture = (Texture2D)EditorGUIUtility.Load(name);
			_iconTextureCacheDict[name] = texture;
			return texture;
		}

		public static Texture2D GetCustom(string name)
		{
			if (_iconTextureCacheDict.TryGetValue(name, out var custom))
				return custom;

			Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(string.Format("Assets/Editor/EditorExtensions/EditorTextures/{0}.png", name));
			_iconTextureCacheDict.Add(name, texture);
			return texture;
		}


		public static Texture2D GetSystem(EEditorIconTextureType editorIconTextureType)
		{
			string name = EditorIconTextureConst.IconTextureNames[(int)editorIconTextureType];
			if (_iconTextureCacheDict.TryGetValue(name, out var system))
				return system;

			Texture2D texture = EditorGUIUtility.FindTexture(name);
			_iconTextureCacheDict.Add(name, texture);
			return texture;
		}

	}
}
#endif





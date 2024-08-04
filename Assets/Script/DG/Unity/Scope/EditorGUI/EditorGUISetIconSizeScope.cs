#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace DG
{
	public class EditorGUISetIconSizeScope : IDisposable
	{
		private readonly Vector2 _preSize;

		public EditorGUISetIconSizeScope(Vector2 newSize)
		{
			_preSize = EditorGUIUtility.GetIconSize();
			EditorGUIUtility.SetIconSize(newSize);
		}


		public void Dispose()
		{
			EditorGUIUtility.SetIconSize(_preSize);
		}
	}
}
#endif